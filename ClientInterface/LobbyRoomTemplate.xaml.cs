using System;
using System.ServiceModel;
using System.Windows.Controls;
using BusinessTier;
using LobbyDatabase;

namespace ClientInterface
{   
    public partial class LobbyRoomTemplate : Page
    {
        private IBusinessInterface foob;
        public LobbyRoomTemplate(String lobbyName)
        {
            InitializeComponent();
            ChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new ChannelFactory<IBusinessInterface>(tcp, url);
            foob = foobFactory.CreateChannel();

            Lobby currentLobby = foob.getLobby(lobbyName);

            roomName.Text = currentLobby.lobbyName;
            roomMessages.Text = "";
        }

        private void sendMessage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            roomMessages.Text = roomMessages.Text + chatBox.Text + "\n";
        }
    }
}
