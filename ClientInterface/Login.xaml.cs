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
        private Lobby loginLobby;
        public Login()
        {
            InitializeComponent();
            ChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new ChannelFactory<IBusinessInterface>(tcp, url);
            foob = foobFactory.CreateChannel();

            loginLobby = new Lobby("Default Lobby");
            foob.addLobby(loginLobby); 
        }   
        private void createUserClick(object sender, RoutedEventArgs e)
        {
            string newUsername = enterUsername.Text.ToString();

            if (foob == null)
            {
                MessageBox.Show("Service is not available.");
                return;
            }

            if(newUsername.Equals("Enter a username"))
            {
                MessageBox.Show("Enter a username with atleast 1 character");
            }

            else if (foob.getUser(loginLobby.lobbyName,newUsername))
            {
                User newUser = new User(newUsername);
                foob.addUser(loginLobby.lobbyName,newUser);
                
                LobbyList lobbyList = new LobbyList(newUser);
                this.NavigationService.Navigate(lobbyList);
            }
            else
            {
                MessageBox.Show("Username taken.");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Enter a username")
            {
                textBox.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter a username";
            }
        }
    }
}
