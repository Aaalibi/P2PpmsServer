using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace P2PpmsServer.Objects
{
    public class UdpConnection
    {

        public int port { get; set; }

        public IPAddress ip { get; set; }

        public IPEndPoint ep { get; set; }

        public UdpClient udpClient { get; private set; }

        public UdpConnection(IPAddress _ip, int _port)
        {
            ip = _ip;
            port = _port;

            udpClient = new UdpClient(port);

            ep = new IPEndPoint(ip, port);

        }
        public byte[] ReceiveData(ref IPEndPoint endPoint)
        {
            byte[] data = udpClient.Receive(ref endPoint);
            return data;

        }

        public void SendResponse(byte[] response, ref IPEndPoint ipep)
        {
            udpClient.Send(response, ipep);
        }
    }
}
