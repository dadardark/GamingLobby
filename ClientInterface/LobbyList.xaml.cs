using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using BusinessTier;
using LobbyDatabase;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Media;

namespace ClientInterface
{
    public partial class LobbyList : Page
    {
        private IBusinessInterface foob;
        private User user;
        private CancellationTokenSource cancellationTokenSource;

        public LobbyList(User user)
        {
            InitializeComponent();
            ChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new ChannelFactory<IBusinessInterface>(tcp, url);
            foob = foobFactory.CreateChannel();

            welcomeText.Text = $"Welcome {user.username}.\nSelect a lobby to join";
            profileName.Text = user.username;
            profileIcon.Source = new BitmapImage(new Uri("Resources/mkxScorpion.gif", UriKind.Relative));

            this.user = user;

            cancellationTokenSource = new CancellationTokenSource();
            startGUIRefresh();
        }

        public async void startGUIRefresh()
        {
            await refreshGUI(cancellationTokenSource.Token);
        }

        private async Task refreshGUI(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    List<string> allLobbies = foob.getAllLobbies();

                    // Update UI on the main thread
                    await Dispatcher.InvokeAsync(() =>
                    {
                        lobbyListPanel.Children.Clear();
                        foreach (string lobbyName in allLobbies)
                        {
                            Button lobbyButton = new Button
                            {
                                Content = lobbyName,
                                Width = 200,
                                Margin = new Thickness(0, 10, 0, 0),
                                Style = (Style)FindResource("MKButtonStyle")
                            };
                            lobbyButton.Foreground = new SolidColorBrush(Colors.Black);
                            lobbyButton.Click += lobbyButton_Click;
                            lobbyListPanel.Children.Add(lobbyButton);
                        }
                    });

                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
            }
            catch (TaskCanceledException)
            {
                // Task was canceled, do nothing
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in refreshing lobby list: {ex.Message}");
            }
        }

        private void lobbyButton_Click(object sender, RoutedEventArgs e)
        {
            string lobbyName = (sender as Button).Content.ToString();
            foob.addUser(lobbyName, user);
            this.NavigationService.Navigate(new LobbyRoomTemplate(lobbyName, user));
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel();
            List<string> allLobbies = foob.getAllLobbies();
            foreach (string lobbyName in allLobbies)
            {
                foob.removeUser(lobbyName, user.username);
            }
            this.NavigationService.GoBack();
        }

        private void CreateLobbyButton_Click(object sender, RoutedEventArgs e)
        {
            string newLobbyName = newLobbyNameTextBox.Text.Trim();

            // Input validation
            if (string.IsNullOrWhiteSpace(newLobbyName))
            {
                MessageBox.Show("Please enter a lobby name.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (newLobbyName.Length > 30)
            {
                MessageBox.Show("Lobby name must be 30 characters or less.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check for invalid characters
            if (!Regex.IsMatch(newLobbyName, @"^[a-zA-Z0-9\s-_]+$"))
            {
                MessageBox.Show("Lobby name can only contain letters, numbers, spaces, hyphens, and underscores.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Lobby newLobby = new Lobby(newLobbyName);
                bool lobbyCreated = foob.addLobby(newLobby);

                if (lobbyCreated)
                {
                    MessageBox.Show($"Lobby '{newLobbyName}' created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    newLobbyNameTextBox.Clear();
                    // The GUI will be updated automatically in the next refresh cycle
                }
                else
                {
                    MessageBox.Show($"Failed to create lobby '{newLobbyName}'. It may already exist.", "Creation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while creating the lobby: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}