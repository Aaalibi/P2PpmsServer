using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P2PpmsServer.Objects
{
    public class Host
    {

        public IPEndPoint ep { private set; get; }

        public Host(IPEndPoint _endPoint)
        {
            ep = _endPoint;
        }

    }
}
