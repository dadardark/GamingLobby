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

            try
            {
                using (ServiceHost host = new ServiceHost(typeof(BusinessServer), baseAddress))
                {
                    host.AddServiceEndpoint(typeof(IBusinessInterface), new NetTcpBinding(), "");

                    host.Open();
                    Console.WriteLine("Server ready at {0} ", baseAddress);
                    Console.WriteLine("Press 'Q' to quit the server.");

                    while (Console.ReadKey().Key != ConsoleKey.Q) { }

                    host.Close();
                    Console.WriteLine("Server shut down.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
