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
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class BusinessServer : IBusinessInterface
    {
        private List<Lobby> lobbyList;
        public BusinessServer() 
        {
            lobbyList = new List<Lobby>();
        }

        public void addLobby(Lobby inLobby)
        {
            lobbyList.Add(inLobby); 
        }

        public Lobby getLobby(String lobbyName) 
        {
            foreach(var lobby in lobbyList)
            {
                if (lobby != null && lobbyName.Equals(lobby.lobbyName, StringComparison.OrdinalIgnoreCase))
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
                Debug.WriteLine(inUser.username + " is added to " + inLobby.lobbyName);
                return true;
            }
            Debug.WriteLine("User not added");
            return false;
        }

        public bool getUser(String lobbyName, String inUsername) 
        {
            Lobby inLobby = getLobby(lobbyName);

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
}
