using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public static class BeamsBuffer
    {
        public static Object Locker = new Object();
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
            throw new NotImplementedException();
        }
    }
}
