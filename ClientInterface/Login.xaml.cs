using System.Diagnostics;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BusinessTier;
using LobbyDatabase;

namespace ClientInterface
{
    public partial class Login : Page
    {
        IBusinessInterface foob;
        private Lobby loginLobby;
        public Login()
        {
            InitializeComponent();
            ChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new ChannelFactory<IBusinessInterface>(tcp, url);
            foob = foobFactory.CreateChannel();

            loginLobby = new Lobby("LoginLobby");
            foob.addLobby(loginLobby);

            foob.addUser(loginLobby.lobbyName, new User("Jacob"));
        }   
        private void createUserClick(object sender, RoutedEventArgs e)
        {
            string newUsername = enterUsername.Text.ToString();

            if (foob == null)
            {
                MessageBox.Show("Service is not available.");
                return;
            }

            if (foob.getUser(loginLobby.lobbyName,newUsername))
            {
                foob.addUser(loginLobby.lobbyName,new User(newUsername));
                
                LobbyList lobbyList = new LobbyList();
                this.NavigationService.Navigate(lobbyList);
            }
            else
            {
                MessageBox.Show("Username taken.");
            }
        }
            
            
    }
}
