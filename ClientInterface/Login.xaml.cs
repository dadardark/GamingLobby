using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using BusinessTier;

namespace ClientInterface
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        IBusinessInterface foob;
        
        public Login()
        {
            InitializeComponent();
            ChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new ChannelFactory<IBusinessInterface>(tcp, url);
            foob = foobFactory.CreateChannel();
        }
        private void createUserClick(object sender, RoutedEventArgs e)
        {
            if (foob.getUser(enterUsername.Text.ToString()) == true)
            {
                foob.addUser(enterUsername.Text.ToString());
                displayUsername.Visibility = Visibility.Visible;
                displayUsername.Text = "Welcome " + enterUsername.Text;

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
