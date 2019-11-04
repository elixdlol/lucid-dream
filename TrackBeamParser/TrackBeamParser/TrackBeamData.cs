using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public class TrackBeamData
    {
        public int TrackNum { get; set; }
        public byte[] Beam1 { get; set; }
        public byte[] Beam2 { get; set; }
        public double Precentage { get; set; }

    }
}
