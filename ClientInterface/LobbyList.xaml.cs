using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

using BusinessTier;
using LobbyDatabase;

namespace ClientInterface
{ 
    public partial class LobbyList : Page
    {
        private IBusinessInterface foob;
        User user;
        public LobbyList(User user)
        {
            InitializeComponent();
            ChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new ChannelFactory<IBusinessInterface>(tcp, url);
            foob = foobFactory.CreateChannel();

            this.user = user;
        }

        private void foodLobbyButton_Click(object sender, RoutedEventArgs e)
        {
            String lobbyName = (sender as Button).Content.ToString();
            foob.addLobby(new Lobby(lobbyName));
            foob.addUser(lobbyName,user);
            this.NavigationService.Navigate(new LobbyRoomTemplate((sender as Button).Content.ToString()));
        }
    }
}
