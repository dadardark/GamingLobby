using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyDatabase
{
    public class Lobby
    {
        public string lobbyName;
        public List<User> users;

        private Lobby() 
        {
            lobbyName = null;
            users = new List<User>();
        }
        public Lobby(string lobbyName)
        {
            this.lobbyName = lobbyName;
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

        public bool getUserName(String inName)
        {
            foreach(User user in users)
            {
                if (user.username.Equals(inName)) 
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (User user in users)
            {
                yield return user.username;
            }
        }
    }
}
