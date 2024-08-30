using System;
using System.ServiceModel;
using BusinessTier;


namespace GamingLobby
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("net.tcp://localhost:8100/BusinessServer");

            using (ServiceHost host = new ServiceHost(typeof(BusinessServer), baseAddress))
            {
                host.AddServiceEndpoint(typeof(IBusinessInterface), new NetTcpBinding(), "");

                host.Open();
                Console.WriteLine("Server ready at {0} ", baseAddress);
                Console.WriteLine("Press enter to stop the service. ");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
