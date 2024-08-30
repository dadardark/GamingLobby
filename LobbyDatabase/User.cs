using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyDatabase
{
    public class User
    {
        public String username;

        public User()
        {
            username = null;
        }

        public User(String username)
        {
            this.username = username;
        }
    }

}
