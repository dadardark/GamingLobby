using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using LobbyDatabase;

namespace BusinessTier
{
    [ServiceContract]
    public interface IBusinessInterface
    {
        [OperationContract]
        bool addLobby(Lobby inLobby);
        [OperationContract]
        Lobby getLobby(String lobbyName);
        [OperationContract]
        int getSize();
        [OperationContract]
        bool addUser(String lobbyName, User inUser);
        [OperationContract]
        bool getUser(String lobbyName, String inUsername);
        [OperationContract]
        void addMessage(String inLobby, String inMessage);
        [OperationContract]
        void removeUser(String lobbyName, string inUsername);
        [OperationContract]
        bool shareFileStatus(string inLobbyName, string inFileName, byte[] inFileData, string inExtension);
        [OperationContract]
        List<string> getAllFiles(string inLobbyName);
        [OperationContract]
        byte[] downloadFile(string inLobbyName, string inFileName);
        [OperationContract]
        void SendPrivateMessage(string lobbyName, string sender, string recipient, string message);
        [OperationContract]
        Dictionary<string, List<string>> GetPrivateMessages(string lobbyName, string user1, string user2);
        [OperationContract]
        List<string> getAllLobbies();
    }     
}
