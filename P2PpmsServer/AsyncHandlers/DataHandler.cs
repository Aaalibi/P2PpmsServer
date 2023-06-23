using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using P2PpmsServer.Objects;

namespace P2PpmsServer.AsyncHandlers
{
    public class DataHandler
    {


        public UdpConnection udpConnection { get; private set; }

        public IPEndPoint endPoint { get; private set; }

        private List<byte[]> dataQueue = new List<byte[]>();

        private ServerMain mainClass { get; set; }

        private DataProcesser dataProcesser;

        public DataHandler(UdpConnection _udpConn, IPEndPoint _ep, ServerMain _mainClass)
        {

            udpConnection = _udpConn;
            endPoint = _ep;
            mainClass = _mainClass;
        }

        public async Task Start(IPEndPoint endPoint)
        {
            Console.WriteLine("Data Handler started. Server ready.");

            dataProcesser = new DataProcesser(udpConnection, endPoint, mainClass);

            while (true)
            {
                if (dataQueue.Count > 0)
                    await dataProcesser.ProcessData(dataQueue[0], endPoint);
              

                byte[] data = udpConnection.ReceiveData(ref endPoint);
                dataQueue.Add(data);
                Console.WriteLine("Received data from: " + endPoint.ToString() + " with data: " + Convert.ToInt32(data[0]));
            }
        }

    }
}
