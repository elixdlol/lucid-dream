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

        public CASSubSegment(byte[] subSegment)
        {
            int id, isTen;
            int.TryParse(subSegment[4].ToString(), out id);
            segID = id;
            int dataStartPoint;

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

            for (int i = 0; i < dataStartPoint; i++)
            {
                header[i] = subSegment[i];
            }

            for (int j = 0; j < 1400 - dataStartPoint - isTen; j++)
            {
                data[j] = subSegment[dataStartPoint + j];
            }


        }
    }
}
