using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyDatabase
{
    public class LobbyList
    {
        public List<Lobby> lobbyList;

        public LobbyList()
        {
            lobbyList = new List<Lobby>();
        }

        public void addLobby(Lobby inLobby)
        {
            lobbyList.Add(inLobby);
        }
    }
}
