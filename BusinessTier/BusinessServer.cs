using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using LobbyDatabase;

namespace BusinessTier
{
    public class BusinessServer : IBusinessInterface
    {
        private LobbyList lobbyList;
        public BusinessServer() 
        {
            lobbyList = new LobbyList();
        }

        public void addLobby(String lobbyName)
        {
            Lobby lobby = new Lobby();
            lobby.lobbyName = lobbyName;
            lobbyList.lobbyList.Add(lobby); 
        }

        public void addUser(String username) 
        {
            User user = new User(username);
        }

        public Lobby getLobby(String lobbyName) 
        {
            foreach(Lobby lobby in lobbyList)
            {
                if (lobbyName.Equals(lobby.lobbyName))
                {
                    return lobby;
                }
            }
            return null;
        }

        public bool getUser(String username)
        {
            foreach (Lobby lobby in lobbyList)
            {
                foreach(User user in lobby)
                {
                    if (user.username.Equals(username))
                    {
                        return false;
                    }
                }
            }
            return true;

        }

    }
}
