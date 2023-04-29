using P2PpmsServer.Exceptions;
using P2PpmsServer.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P2PpmsServer.AsyncHandlers
{
    public class DataProcesser
    {
        public UdpConnection udpConnection { get; private set; }

        public IPEndPoint endPoint { get; private set; }

        private List<byte[]> dataQueue = new List<byte[]>();

        private ServerMain mainClass { get; set; }

        public DataProcesser(UdpConnection _udpConn, IPEndPoint _ep, ServerMain _mainClass)
        {

            udpConnection = _udpConn;
            endPoint = _ep;
            mainClass = _mainClass;
            
        }

        public async Task ProcessData(byte[] data, IPEndPoint ep)
        {
            byte responseByte = data[0];

            Int32 parsedInt = Convert.ToInt32(responseByte);
            Console.WriteLine($"Processing Data: {responseByte}");

            foreach( Host h in mainClass.hosts)
            {
                if(ep == h.ep)
                {
                    Console.WriteLine("Host already existing.");
                    return;
                }
                else
                {
                    Host newHost = new Host(ep);
                    mainClass.hosts.Add(newHost);
                    Console.WriteLine("New host added.");
                }
            }

            if (parsedInt == (int)IncomingResponse.OK)
            {
                Console.WriteLine("Incoming data = OK, not sending a response to not LOOP.");
                // udpConnection.SendResponse((int)OutgoingResponse.OK, ref udpConnection.ep);
                
            }
            else if (parsedInt == (int)IncomingResponse.ERROR)
            {
                udpConnection.SendResponse((int)OutgoingResponse.OK, ref ep);
            }
            else if (parsedInt == (int)IncomingResponse.MESSAGE)
            {
                udpConnection.SendResponse((int)OutgoingResponse.OK, ref ep);
            }
            else if (parsedInt == (int)IncomingResponse.ASSIGN)
            {
                udpConnection.SendResponse((int)OutgoingResponse.OK, ref ep);
            }
            else
            {

                Console.WriteLine("Packet Type not recognized. Please check: " + parsedInt);
                udpConnection.SendResponse((int)OutgoingResponse.ERROR, ref ep);
                throw new PacketTypeNotFoundException(parsedInt, ep);
                
            }

        }

    }
}
