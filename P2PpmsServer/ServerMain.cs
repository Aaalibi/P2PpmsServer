using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using P2PpmsServer.AsyncHandlers;
using P2PpmsServer.Objects;
using P2PpmsServer.Utilities;
using P2PpmsServer.Utilities.Encryption;

namespace P2PpmsServer
{
    public class ServerMain
    {
        public double version = 1.0;
        UdpConnection udpConnection;
        IPEndPoint endPoint;
        DataHandler dataHandler;
        private byte[] aespassword;
        private byte[] aesIV;
        
        public List<Host> hosts = new List<Host>();
        


        public static async Task Main() 
        {
            var mainClass = new ServerMain();
            Console.WriteLine($"Initializing server with version: {mainClass.version}");

            // mainClass.aespassword = "VtKreVWSBwHVcFvx2YOZC+yt6mKVhkL8";
            
            mainClass.aesIV = new byte[16];
            mainClass.aespassword = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(mainClass.aesIV);
                rng.GetBytes(mainClass.aespassword);
            }
            Console.WriteLine($"Initialized AES IV: {mainClass.aesIV}");
            Console.WriteLine($"Initialized AES Password: {mainClass.aespassword}");

            mainClass.udpConnection = new UdpConnection(IPAddress.Any, 11000);
            mainClass.endPoint = mainClass.udpConnection.ep;

            Console.WriteLine("Initialized port and endpoint.");
            Console.WriteLine("Initializing data handler.");
            Console.WriteLine("Encrypted string: " + AESService.Encrypt("ciao", mainClass.aespassword, mainClass.aesIV));
            mainClass.dataHandler = new DataHandler(mainClass.udpConnection, mainClass.endPoint, mainClass);

            Console.WriteLine("Started data handler.");
            await mainClass.dataHandler.Start(mainClass.endPoint);

            


        }
        

    }
}
