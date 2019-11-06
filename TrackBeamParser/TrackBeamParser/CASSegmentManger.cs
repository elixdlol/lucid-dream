using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public static class CASSegmentManger
    {
        private static CASSegment segToBuffer;

        public static void BufferManger(CASSubSegment subSegmentFromUdp)
        {
            if (subSegmentFromUdp.segID == 1)
            {
                if (segToBuffer != null)
                {
                    if (segToBuffer.IsValid)
                    {
                        //if(segToBuffer.Data.Count < 192*64)
                        //{
                        BeamsBuffer.WriteBeamsFromDictionary(segToBuffer.GetBeamsValues());
                        BeamsBuffer.Heading = segToBuffer.Heading;
                        //}
                    }
                }


                segToBuffer = new CASSegment(subSegmentFromUdp);
            }
            else
            {
                if (segToBuffer != null)
                {
                    segToBuffer.AddSubSegment(subSegmentFromUdp);
                }
            }
        }
    }
}
