using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using LobbyDatabase;

namespace BusinessTier
{
    [ServiceContract(CallbackContract = typeof(IClientCallback))]
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
    }

    [ServiceContract]
    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void userAdded(String lobbyname, User user);
    }
}
