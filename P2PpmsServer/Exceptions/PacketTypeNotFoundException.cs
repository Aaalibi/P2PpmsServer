using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P2PpmsServer.Exceptions
{
    [Serializable]
    public class PacketTypeNotFoundException : Exception
    {

        public PacketTypeNotFoundException(int packetType, IPEndPoint ep) { }

    }
}
