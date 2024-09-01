using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyDatabase
{
    public class Lobby
    {
        public string lobbyName;
        public List<User> users;

        public Lobby() 
        {
            lobbyName = null;
            users = new List<User>();
        }

        public void setName(String inName)
        {
            lobbyName = inName;
        }

        public void addUser(User inUser)
        {
            users.Add(inUser);  
        }

        public string getName()
        {
            return lobbyName;
        }
    }
}
