using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public static class BeamsBuffer
    {
        static byte[][] Beams = new byte[beamsNumber][];
        const int beamsNumber= 192;
        public static Object Locker = new Object();

        public static void writeBeams(byte[][] beams)
        {
            // append to end of every beam array
        }

        public static byte[][] getBeamsAndFlush()
        {
            lock (Locker)
            {
                byte[][] beamArray = new byte[][] { };
                cleanBeams();
                return beamArray;
            }
        }

        private static void cleanBeams()
        {
            Beams = new byte[beamsNumber][];
        }
    }
}
