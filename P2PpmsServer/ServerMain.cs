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
        


        public static async Task Main() 
        {
            var mainClass = new ServerMain();
            
            mainClass.udpConnection = new UdpConnection(IPAddress.Any, 11001);
            mainClass.endPoint = mainClass.udpConnection.ep;

            mainClass.dataHandler = new DataHandler(mainClass.udpConnection, mainClass.endPoint, mainClass);
            

            await mainClass.dataHandler.Start(mainClass.endPoint);
            

        }
        

    }
}
