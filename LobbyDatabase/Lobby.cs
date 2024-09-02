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
        private static Lobby _instance;
        private static readonly object _lock = new object();

        private Lobby() 
        {
            lobbyName = null;
            users = new List<User>();
        }
        public static Lobby Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Lobby();
                    }
                    return _instance;
                }
            }
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
