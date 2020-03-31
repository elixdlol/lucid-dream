using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterfallSimulator
{
    public class LSRHeaderGenerator
    {
        public byte[] CreateHeader()
        {
            byte[] header = new byte[24];

            header[0] = 0;
            header[1] = 4;
            header[2] = 4;

            for (int i = 3; i < header.Length; i++)
            {
                header[i] = 0;
            }
            return header;
        }
    }
}
