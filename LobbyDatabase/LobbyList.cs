using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyDatabase
{
    public class LobbyList : IEnumerable
    {
        public List<Lobby> lobbyList;
        public int size = 0;

        public LobbyList()
        {
            lobbyList = new List<Lobby>();
        }

        public void addLobby(Lobby inLobby)
        {
            lobbyList.Add(inLobby);
            size++;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Lobby lobby in lobbyList)
            {
                yield return lobby.getName();
            }
        }

        public int getSize()
        {
            return size;
        }
    }
}
