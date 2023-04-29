using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using P2PpmsServer.AsyncHandlers;
using P2PpmsServer.Utilities;

namespace P2PpmsServer
{
    public class ServerMain
    {
        public double version = 1.0;
        UdpConnection udpConnection;
        IPEndPoint endPoint;
        DataHandler dataHandler;
        // TODO: Scrivere un oggetto Host che richiami la connessione, indirizzo e porta
        public List<Host> hosts = new List<Host>();
        


        public static async Task Main() 
        {
            var mainClass = new ServerMain();
            Console.WriteLine($"Initializing server with version: {mainClass.version}");

            mainClass.udpConnection = new UdpConnection(IPAddress.Any, 11001);
            mainClass.endPoint = mainClass.udpConnection.ep;

            Console.WriteLine("Initialized port and endpoint.");
            Console.WriteLine("Initializing data handler.");
            mainClass.dataHandler = new DataHandler(mainClass.udpConnection, mainClass.endPoint, mainClass);

            Console.WriteLine("Started data handler.");
            await mainClass.dataHandler.Start(mainClass.endPoint);

            


        }
        

    }
}
