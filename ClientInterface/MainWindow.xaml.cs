using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BusinessTier;

namespace ClientInterface
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                LobbyList lobbyList = new LobbyList();
                Login login = new Login();
            
                Content = login;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }

        
    }
}
