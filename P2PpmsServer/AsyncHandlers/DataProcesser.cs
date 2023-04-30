using P2PpmsServer.Exceptions;
using P2PpmsServer.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            byte[] outgoingBuffer = new byte[1024];

            // Retrieving response status and parsing it
            byte responseByte = data[0];

            Int32 parsedInt = Convert.ToInt32(responseByte);
            Console.WriteLine($"Processing Data: {responseByte}");

            

            switch(parsedInt)
            { 

                case (int)IncomingResponseHost.HELLO:
                    if (mainClass.hosts.Count == 0)
                    {
                        Host h = new Host(ep);
                        mainClass.hosts.Add(h);
                        Console.WriteLine($"First host added to list. {h.ep}");
                    }
                    else
                    {
                        Console.WriteLine("---------------1");
                        foreach (Host ho in mainClass.hosts)
                            Console.WriteLine(ho.ep);

                        Console.WriteLine("---------------1");

                        Host h = new Host(ep);
                        if (!mainClass.hosts.Contains(h))
                        {
                            mainClass.hosts.Add(h);
                            Console.WriteLine($"New host added. {h.ep}");
                        }
                        else
                        {
                            Console.WriteLine("Host already existing.");
                        }

                        Console.WriteLine("---------------2");
                        foreach (Host ho in mainClass.hosts)
                            Console.WriteLine(ho.ep);

                        Console.WriteLine("---------------2");
                        Console.WriteLine();
                    }

                    outgoingBuffer[0] = Convert.ToByte(OutgoingResponseHost.WELCOME);
                    break;


                case (int)IncomingResponseHost.OK:
                    Console.WriteLine("Incoming data = OK.");

                    outgoingBuffer[0] = Convert.ToByte(OutgoingResponseHost.OK);
                    break;

                case (int)IncomingResponseHost.ERROR:
                    outgoingBuffer[0] = Convert.ToByte(OutgoingResponseHost.OK);
                    break;

                case (int)IncomingResponseHost.MESSAGE:
                    outgoingBuffer[0] = Convert.ToByte(OutgoingResponseHost.OK);
                    break;

                default:
                    Console.WriteLine("Packet Type not recognized. Please check: " + parsedInt);
                    outgoingBuffer[0] = Convert.ToByte(OutgoingResponseHost.ERROR);
                    throw new PacketTypeNotFoundException(parsedInt, ep);


             
            }

            udpConnection.SendResponse(outgoingBuffer, ref ep);
            
        }

    }
}
