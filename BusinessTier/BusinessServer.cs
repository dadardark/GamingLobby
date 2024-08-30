using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void addUser(String username, String lobbyName) 
        {

        }

    }
}
