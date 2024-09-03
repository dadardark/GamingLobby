using System;
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
        public BusinessServer() 
        {
            lobbyList = new List<Lobby>();
        } 
        public bool addLobby(Lobby inLobby)
        {
            foreach (var lobby in lobbyList)
            {
                if (lobby != null && lobby.lobbyName.Equals(inLobby.lobbyName, StringComparison.OrdinalIgnoreCase))
                {   
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
                return true;
            }    
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

            foreach (User user in inLobby.users)
            {
  
                if (user.username.Equals(inUsername))
                { 
                    return false;
                }
            }
            return true;
        }

        public void removeUser(String lobbyName, string inUsername)
        {
            Lobby inLobby = getLobby(lobbyName);
            foreach (User user in inLobby.users.ToList())
            {  
                if (user.username.Equals(inUsername))
                {
                    inLobby.users.Remove(user);  
                }
            }   
        }

        public void addMessage(String inLobby,String inMessage)
        {
            Lobby lobby = getLobby(inLobby);
            lobby.addMessage(inMessage);
        }
        public int getSize()
        {
            return lobbyList.Count;
        }
    }
}
