using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public class CASSubSegment
    {
        public int segID;
        public byte[] header;
        public byte[] data;
        public byte[] heading;

        public CASSubSegment(byte[] subSegment)
        {
            int id, isTen;
            int.TryParse(subSegment[4].ToString(), out id);
            segID = id;
            int dataStartPoint;
            int headingStartPoint = 1224;

            isTen = 0;
            switch (id)
            {
                case 1:
                    dataStartPoint = 256;
                    break;

                case 10:
                    isTen = 16;
                    dataStartPoint = 32;
                    break;

                default:
                    dataStartPoint = 32;
                    break;
            }
            header = new byte[dataStartPoint];
            data = new byte[1400 - dataStartPoint - isTen];
            heading = new byte[2];

            for (int i = 0; i < dataStartPoint; i++)
            {
                header[i] = subSegment[i];
            }

            for (int j = 0; j < 1400 - dataStartPoint - isTen; j++)
            {
                data[j] = subSegment[dataStartPoint + j];
            }

            if (segID == 10)
            {
                for (int i = 0; i < 2; i++)
                {
                    heading[i] = subSegment[headingStartPoint + dataStartPoint + i];
                }
            }

        }
    }
}
