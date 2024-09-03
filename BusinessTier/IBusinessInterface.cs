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
    }     
}
