using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackBeamParser
{
    class CASSegment
    {
        public bool IsValid;
        public List<byte> Data;
        public Dictionary<int, CASSubSegment> SubSegments;
        public double Heading { get; set; }

        public CASSegment(CASSubSegment subSegmentToAdd)
        {
            IsValid = true;
            Data = new List<byte>();
            SubSegments = new Dictionary<int, CASSubSegment>();
            Data.AddRange(subSegmentToAdd.data);
            SubSegments.Add(1, subSegmentToAdd);
        }

        public void AddSubSegment(CASSubSegment subSegmentToAdd)
        {
            if (subSegmentToAdd.segID == SubSegments.Last().Key + 1)
            {
                try
                {
                    if(subSegmentToAdd.segID == 10)
                    {
                        Heading = BitConverter.ToInt16(subSegmentToAdd.heading);
                    }
                    Data.AddRange(subSegmentToAdd.data);
                    SubSegments.Add(subSegmentToAdd.segID, subSegmentToAdd);
                }
                catch
                {
                    IsValid = false;
                }
                
            }
            else
            {
                IsValid = false;
            }
        }

        public byte[][] GetBeamsValues()
        {
            byte[][] beams = new byte[192][];
            Dictionary<int, List<byte>> dictionaryToSend = new Dictionary<int, List<byte>>();

            for (int i = 0; i < 192; i++)
            {
                beams[i] = new byte[64];
                Array.Copy(Data.GetRange(i * 64, 64).ToArray(), beams[i], 64);
            }

            return beams;

        }

    }
}
