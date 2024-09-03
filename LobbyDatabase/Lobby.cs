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
        public List<String> messages;

        private Lobby() 
        {
            lobbyName = null;
            users = new List<User>();
            messages = new List<String>();
        }
        public Lobby(string lobbyName)
        {
            this.lobbyName = lobbyName;
            users = new List<User>();
            messages = new List<String>();
        }

        public void setName(String inName)
        {
            lobbyName = inName;
        }

        public void addUser(User inUser)
        {
            users.Add(inUser);  
        }

        public void addMessage(String inMessage)
        {
            messages.Add(inMessage);
        }
        
        public string getName()
        {
            return lobbyName;
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
