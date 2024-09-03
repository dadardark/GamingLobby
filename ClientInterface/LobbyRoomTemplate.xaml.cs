using System;
using System.ServiceModel;
using System.Windows.Controls;
using System.Windows;
using BusinessTier;
using LobbyDatabase;

namespace ClientInterface
{   
    public partial class LobbyRoomTemplate : Page
    {
        private IBusinessInterface foob;
        Lobby currentLobby;
        public LobbyRoomTemplate(String lobbyName)
        {
            InitializeComponent();
            ChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new ChannelFactory<IBusinessInterface>(tcp, url);
            foob = foobFactory.CreateChannel();

            currentLobby = foob.getLobby(lobbyName);

            lobbyTitle.Text = currentLobby.lobbyName;

            foreach (User user in currentLobby.users)
            {
                lobbyUsers.Items.Add(user.username);
            } 
        }

        private void sendMessage_Click(object sender, RoutedEventArgs e)
        {
            lobbyMessages.Items.Add(enterMessage.Text.ToString());   
        }

        private void updateGUI_Click(object sender, RoutedEventArgs e)
        {
            currentLobby = foob.getLobby(lobbyTitle.Text);
            foreach (User user in currentLobby.users)
            {
                if (!lobbyUsers.Items.Contains(user.username))
                {
                    lobbyUsers.Items.Add(user.username);
                }
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
