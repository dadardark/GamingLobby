﻿using System;
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
        public LobbyList()
        {
            InitializeComponent();
            ChannelFactory<IBusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string url = "net.tcp://localhost:8100/BusinessServer";
            foobFactory = new ChannelFactory<IBusinessInterface>(tcp, url);
            foob = foobFactory.CreateChannel();
        }

        private void foodLobbyButton_Click(object sender, RoutedEventArgs e)
        {
            foob.addLobby(new Lobby((sender as Button).Content.ToString()));
            this.NavigationService.Navigate(new LobbyRoomTemplate((sender as Button).Content.ToString()));
        }
    }
}
