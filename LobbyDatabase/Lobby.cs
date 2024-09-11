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
        private List<PrivateMessage> privateMessages;

        private Lobby() 
        {
            lobbyName = null;
            users = new List<User>();
            messages = new List<String>();
            sharedFiles = new Dictionary<string, Tuple<byte[], string>>();
            privateMessages = new List<PrivateMessage>();
        }
        public Lobby(string lobbyName)
        {
            this.lobbyName = lobbyName;
            users = new List<User>();
            messages = new List<String>();
            sharedFiles = new Dictionary<string, Tuple<byte[], string>>();
            privateMessages = new List<PrivateMessage>();
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

        public void addPrivateMessage(string inSender,string inReceipient, string inMessage)
        {
            privateMessages.Add(new PrivateMessage(inSender, inReceipient, inMessage));
        }

        public Dictionary<string, List<string>> getPrivateMessages(string user1, string user2)
        {
            var messages = privateMessages.Where(pm =>
                (pm.sender == user1 && pm.recepient == user2) ||
                (pm.sender == user2 && pm.recepient == user1))
                .OrderBy(pm => pm.timeStamp);

            var result = new Dictionary<string, List<string>>();
            result[user1] = new List<string>();
            result[user2] = new List<string>();

            foreach (var pm in messages)
            {
                string formattedMessage = $"[{pm.timeStamp}] {pm.sender}: {pm.message}";
                if (pm.sender == user1)
                {
                    result[user1].Add(formattedMessage);
                }
                else
                {
                    result[user2].Add(formattedMessage);
                }
            }

            return result;
        }
    }
}
