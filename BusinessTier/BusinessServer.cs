﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Windows;
using LobbyDatabase;

namespace BusinessTier
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)] 
    public class BusinessServer : IBusinessInterface
    {
        private List<Lobby> lobbyList;
        private List<IClientCallback> callbacks = new List<IClientCallback>();
        public BusinessServer() 
        {
            lobbyList = new List<Lobby>();
        }

        public IClientCallback getCallback()
        {
            return OperationContext.Current.GetCallbackChannel<IClientCallback>();
        }

        private void NotifyClients(string lobbyName, User user)
        {
            foreach (var callback in callbacks)
            {   
                callback.userAdded(lobbyName, user);
            }
        }

        public bool addLobby(Lobby inLobby)
        {
            foreach (var lobby in lobbyList)
            {
                if (lobby != null && lobby.lobbyName.Equals(inLobby.lobbyName, StringComparison.OrdinalIgnoreCase))
                {
                    Debug.WriteLine("Lobby Exists already. Not added");
                    return false;
                }
            }
            lobbyList.Add(inLobby);
            return true;
        }

        public Lobby getLobby(String lobbyName) 
        {
            foreach(var lobby in lobbyList)
            {
                if (lobby != null && lobbyName.Equals(lobby.lobbyName))
                {
                    return lobby;
                }
            }
            Debug.WriteLine("Lobby not found");
            return null;
        }

        public bool addUser(String lobbyName, User inUser)
        {
            Lobby inLobby = getLobby(lobbyName);

            bool exisitingUser = getUser(lobbyName, inUser.username);

            if (exisitingUser)
            {
                inLobby.users.Add(inUser);
                NotifyClients(lobbyName, inUser);
                return true;
            }
            Debug.WriteLine("User not added");
            return false;
        }

        public bool getUser(String lobbyName, String inUsername) 
        {
            Lobby inLobby = getLobby(lobbyName);
            
            if(inLobby == null)
            {
                Debug.WriteLine("Not returning a lobby");
                return false;
            }


            Debug.WriteLine("getUser: Lobby name: "+inLobby.lobbyName);

            foreach (User user in inLobby.users)
            {
                Debug.WriteLine("getUser: User name: " + user.username);
                if (user.username.Equals(inUsername))
                {
                    Debug.WriteLine("User exists");
                    return false;
                }
            }
            return true;
        }
        public int getSize()
        {
            return lobbyList.Count;
        }

    }

    public class ClientCallBack : IClientCallback
    {
        public void userAdded(String lobbyName, User user) 
        {
            Debug.WriteLine(user.username + " added to lobby: "+lobbyName);
        }
    }
}
