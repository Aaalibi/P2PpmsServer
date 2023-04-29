using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PpmsServer.Utilities
{
    public class DataByteToString
    {


        public static string TransformData(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }
    }
}
