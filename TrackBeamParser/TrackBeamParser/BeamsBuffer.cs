using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public static class BeamsBuffer
    {
        const int beamsNumber = 192;
        const int maxBufferSize = 1400 * 10 * 1000 * 10;
        static int indexOfEndInBuffers;
        static byte[][] Beams;
        public static Object Locker = new Object();

        static BeamsBuffer()
        {
            cleanBeams();
        }

        public static void WriteBeamsFromDictionary(byte[][] beamsValues)
        {
            lock (Locker)
            {
                int j = 0;

                foreach (var beam in beamsValues)
                {
                    Buffer.BlockCopy(beam, 0, Beams[j], indexOfEndInBuffers, beam.Length);
                    indexOfEndInBuffers += beam.Length;
                    j++;
                }
            }
        }

        public static byte[][] getBeamsAndFlush()
        {
            lock (Locker)
            {
                byte[][] beamArray = Beams;
                cleanBeams();
                return beamArray;
            }
        }

        private static void cleanBeams()
        {
            indexOfEndInBuffers = 0;
            Beams = new byte[beamsNumber][];

            for (int i = 0; i < Beams.Length; i++)
            {
                Beams[i] = new byte[maxBufferSize];
            }
        }
    }
}
