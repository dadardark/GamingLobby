using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using BusinessTier;
using LobbyDatabase;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO.Packaging;

namespace ClientInterface
{   
    public partial class LobbyRoomTemplate : Page
    {
        private IBusinessInterface foob;
        private CancellationTokenSource cancellationTokenSource;
        private readonly string[] extensions = { ".jpg", ".jpeg", ".png", ".txt" }; //readonly = final in java
        private string selectedUser;
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
            enterMessage.Clear();
        }

        private void updateGUI_Click(object sender, RoutedEventArgs e)
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
            foreach(String message in currentLobby.messages)
            {
                if (!lobbyMessages.Items.Contains(message))
                {
                    lobbyMessages.Items.Add(message);
                }
            }
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
                    List<string> sharedFiles = foob.getAllFiles(currentLobby.lobbyName);
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

                    foreach (string fileName in sharedFiles)
                    {
                        if (!sharedFilesListView.Items.Contains(fileName))
                        {
                            sharedFilesListView.Items.Add(fileName);
                        }
                    }
                    if (!string.IsNullOrEmpty(selectedUser))
                    {
                        receipient.Text = selectedUser;
                        loadPM();
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

        private async void shareFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Acceptable Files|*.jpg;*.jpeg;*.png;*.txt"
            };
            if (dialog.ShowDialog() == true)
            {
                string fileName = Path.GetFileName(dialog.FileName);
                string extension = Path.GetExtension(dialog.FileName).ToLower();

                if (extensions.Contains(extension))
                {
                    List<string> currentFiles = await Task.Run(() => foob.getAllFiles(currentLobby.lobbyName));

                    if(currentFiles.Contains(fileName))
                    {
                        MessageBox.Show("Failed to upload the file : File with the same name uploaded before");
                    }
                    else
                    {
                        byte[] fileData = File.ReadAllBytes(dialog.FileName);
                        bool status = foob.shareFileStatus(currentLobby.lobbyName, fileName, fileData, extension);
                        if (status)
                        {
                            MessageBox.Show($"File '{fileName}' uploaded !");
                            startGUIRefresh();
                        }
                        else
                        {
                            MessageBox.Show("Failed to upload the file.");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Only .jpg, .jpeg, .png, or .txt file. are Acceptable! ");
                }
            }
        }

        private void sharedFilesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is string fileName)
            {
                byte[] data = foob.downloadFile(currentLobby.lobbyName, fileName);
                if (data != null)
                {
                    string extension = Path.GetExtension(fileName).ToLower();
                    SaveFileDialog downloadDialog = new SaveFileDialog
                    {
                        FileName = fileName,
                    };
                    if (downloadDialog.ShowDialog() == true)
                    {
                        File.WriteAllBytes(downloadDialog.FileName, data);
                        MessageBox.Show($"File '{fileName}' downloaded successfully!");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to download the file.");
                }
            }
        }

        private async void sendPMButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(selectedUser))
            {
                MessageBox.Show("Please DoubleClickto select a Receipient!");
                return;
            }
            string message = pmTextBox.Text;
            if(string.IsNullOrEmpty(message))
            {

            }
            else
            {
                foob.SendPrivateMessage(currentLobby.lobbyName, inUser.username, selectedUser, message);
                pmTextBox.Clear();
                loadPM();
            }
        }

        private void lobbyUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is string selectedUser)
            {
                if (selectedUser.Equals(inUser.username))
                {
                    MessageBox.Show("Please select user other than you");
                    return;
                }
                else
                {
                    receipient.Text = selectedUser;
                    this.selectedUser = selectedUser;
                    loadPM();

                }

            }
        }

        private void loadPM()
        {
            if (!string.IsNullOrEmpty(selectedUser))
            {
                Dictionary<string, List<string>> messages = foob.GetPrivateMessages(currentLobby.lobbyName, inUser.username, selectedUser);

                pmListView.Items.Clear();
                if (messages.ContainsKey(inUser.username))
                {
                    foreach (var message in messages[inUser.username])
                    {
                        pmListView.Items.Add(message);
                    }
                }
                if (messages.ContainsKey(selectedUser))
                {
                    foreach (var message in messages[selectedUser])
                    {
                        pmListView.Items.Add(message);
                    }
                }
                if (pmListView.Items.Count > 0)
                {
                    pmListView.ScrollIntoView(pmListView.Items[pmListView.Items.Count - 1]);
                }
            }
        }
    }
}
