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
        private IBusinessInterface foob;
        private IClientCallback callBack;
        private Lobby loginLobby;
        public Login()
        {
            InitializeComponent();
            DuplexChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            callBack = new ClientCallBack();
            InstanceContext instanceContext = new InstanceContext(callBack);

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new DuplexChannelFactory<IBusinessInterface>(instanceContext,tcp, url);
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
