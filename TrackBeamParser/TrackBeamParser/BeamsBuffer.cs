using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public static class BeamsBuffer
    {
        const int beamsNumber = 192;
        const int maxBufferSize = 1000000;
        static volatile int indexOfEndInBuffers;
        static volatile byte[][] Beams;
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
                int beamLength = beamsValues[0].Length;

                foreach (var beam in beamsValues)
                {
                    Buffer.BlockCopy(beam, 0, Beams[j], indexOfEndInBuffers, beamLength);
                    j++;
                }

                indexOfEndInBuffers += beamLength;
                Console.WriteLine("BUFFER SIZE: " + indexOfEndInBuffers);
            }
        }

        public static byte[][] getBeamsAndFlush()
        {
            lock (Locker)
            {
                byte[][] beamArray = new byte[beamsNumber][];
                int j = 0;

                foreach (var beam in Beams)
                {
                    beamArray[j] = new byte[indexOfEndInBuffers];
                    Buffer.BlockCopy(beam, 0, beamArray[j], 0, indexOfEndInBuffers);
                    j++;
                }

                cleanBeams();
                return beamArray;
            }
        }

        private static void cleanBeams()
        {
            Console.WriteLine("*** CLEANING BUFFER ***");
            indexOfEndInBuffers = 0;
            Beams = new byte[beamsNumber][];

            for (int i = 0; i < Beams.Length; i++)
            {
                Beams[i] = new byte[maxBufferSize];
            }
        }
    }
}
