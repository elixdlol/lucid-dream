using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;

namespace TrackBeamParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(()=>
            {
                TracksDataReceiver.StartListening((trackData)=>
                {
                    BeamMaker.onReceiveTracks(trackData);
                });
            });
            thread.Start();


            byte[] a = File.ReadAllBytes(@"C:\Users\96ron\Desktop\האקויק ראגב\CAS_HAKATON.rec");

            int i = 0;
            while (i < a.Length)
            {
                byte[] part = new byte[1400];
                Buffer.BlockCopy(a, i, part, 0, 1400);
                NOP(0.0001);
                CASSubSegment newCAS = new CASSubSegment(part);
                CASSegmentManger.BufferManger(newCAS);
                i += 1400;
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static void NOP(double durationSeconds)
        {
            var durationTicks = Math.Round(durationSeconds * Stopwatch.Frequency);
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedTicks < durationTicks)
            {

            }
        }

        public static class NonBlockingConsole
        {
            private static BlockingCollection<string> m_Queue = new BlockingCollection<string>();

            static NonBlockingConsole()
            {
                var thread = new Thread(
                  () =>
                  {
                      while (true) Console.WriteLine(m_Queue.Take());
                  });
                thread.IsBackground = true;
                thread.Start();
            }

            public static void WriteLine(string value)
            {
                m_Queue.Add(value);
            }
        }
    }
}
