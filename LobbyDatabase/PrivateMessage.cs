using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LobbyDatabase
{
    public class PrivateMessage
    {
        public string sender { get; set; }
        public string recepient { get; set; }
        public string message { get; set; }
        public DateTime timeStamp { get; set; }

        public PrivateMessage(string inSender, string inRecepient, string inMessage)
        {
            sender = inSender;
            recepient = inRecepient;
            message = inMessage;
            timeStamp = DateTime.Now;
        }

        public PrivateMessage() { }
    }
}
