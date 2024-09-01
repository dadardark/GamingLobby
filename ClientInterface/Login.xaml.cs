using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            foob.addUser(enterUsername.Text.ToString());
            displayUsername.Visibility = Visibility.Visible;
            displayUsername.Text = "Welcome " + enterUsername.Text;
            Thread.Sleep(2000);
            
            

        }
    }
}
