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
        private Dictionary<string, Tuple<byte[], string>> sharedFiles;

        private Lobby() 
        {
            lobbyName = null;
            users = new List<User>();
            messages = new List<String>();
            sharedFiles = new Dictionary<string, Tuple<byte[], string>>();
        }
        public Lobby(string lobbyName)
        {
            this.lobbyName = lobbyName;
            users = new List<User>();
            messages = new List<String>();
            sharedFiles = new Dictionary<string, Tuple<byte[], string>>();
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
        public List<string> getAllFiles()
        {
            return new List<string>(sharedFiles.Keys);
        }

        public void addFile(string inFileName, byte[] inFileData, string inExtension)
        {
            sharedFiles[inFileName] = new Tuple<byte[], string>(inFileData, inExtension);
        }

        public byte[] getFile(string inFileName)
        {
            if (sharedFiles.TryGetValue(inFileName, out var fileTuple))
            {
                return fileTuple.Item1;
            }
            return null;
        }

        public string getExtension(string inFileName)
        {
            if (sharedFiles.TryGetValue(inFileName, out var fileTuple))
            {
                return fileTuple.Item2;
            }
            return null;
        }
    }
}
