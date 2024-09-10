using System;
using System.ServiceModel;
using System.Windows.Controls;
using System.Windows;
using BusinessTier;
using LobbyDatabase;
using System.Threading.Tasks;
using System.Threading;

namespace ClientInterface
{   
    public partial class LobbyRoomTemplate : Page
    {
        private IBusinessInterface foob;
        private CancellationTokenSource cancellationTokenSource;
        Lobby currentLobby;
        User inUser;
        public LobbyRoomTemplate(String lobbyName, User inUser)
        {
            InitializeComponent();
            ChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new ChannelFactory<IBusinessInterface>(tcp, url);
            foob = foobFactory.CreateChannel();

            this.inUser = inUser;

            currentLobby = foob.getLobby(lobbyName);

            lobbyTitle.Text = currentLobby.lobbyName;
            cancellationTokenSource = new CancellationTokenSource();
            startGUIRefresh();
        }

        private void sendMessage_Click(object sender, RoutedEventArgs e)
        {
            foob.addMessage(lobbyTitle.Text, ("[" + DateTime.Now + "] " + inUser.username + " : " + enterMessage.Text.ToString()));
            lobbyMessages.Items.Add("["+DateTime.Now+"] "  + inUser.username + " : " + enterMessage.Text.ToString());
        }

        public async void startGUIRefresh()
        {
            await refreshGUI(cancellationTokenSource.Token);
        }

        private async Task refreshGUI(CancellationToken cancellationtoken)
        {
            try
            {
                while (!cancellationtoken.IsCancellationRequested)
                {
                    currentLobby = foob.getLobby(lobbyTitle.Text);
                    lobbyUsers.Items.Clear();

                    foreach (User user in currentLobby.users)
                    {
                        if (!lobbyUsers.Items.Contains(user.username))
                        {
                            lobbyUsers.Items.Add(user.username);
                        }
                    }
                    foreach (String message in currentLobby.messages)
                    {
                        if (!lobbyMessages.Items.Contains(message))
                        {
                            lobbyMessages.Items.Add(message);
                        }
                    }

                    await Task.Delay(TimeSpan.FromSeconds(0.1), cancellationtoken);
                }
            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in loading lobby" + ex.ToString());
            }
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            foob.removeUser(lobbyTitle.Text, inUser.username);
            this.NavigationService.GoBack();
        }
    }
}
