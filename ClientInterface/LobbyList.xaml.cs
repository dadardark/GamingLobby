using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
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
using LobbyDatabase;

namespace ClientInterface
{ 
    public partial class LobbyList : Page
    {
        private IBusinessInterface foob;
        private Lobby loginLobby;
        public LobbyList()
        {
            InitializeComponent();
            ChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new ChannelFactory<IBusinessInterface>(tcp, url);
            foob = foobFactory.CreateChannel();
        }

        private void lobby1Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
