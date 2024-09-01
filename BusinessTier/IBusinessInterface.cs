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
        void addUser(String inName);
        [OperationContract]
        void addLobby(String lobbyName);
        [OperationContract]
        bool getUser(String username);


    }
}
