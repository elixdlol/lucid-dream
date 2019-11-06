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
            Thread thread = new Thread(() =>
            {
                MicroLibrary.MicroTimer microTimer = new MicroLibrary.MicroTimer();
                microTimer.MicroTimerElapsed +=
                    new MicroLibrary.MicroTimer.MicroTimerElapsedEventHandler(OnTimedEvent);

                microTimer.Interval = 1000000; // Call micro timer every 1000µs (1ms)
                microTimer.Enabled = true; // Start timer
            });
            thread.Start();

            byte[] a = File.ReadAllBytes(@"C:\Users\96ron\Desktop\האקויק ראגב\CAS_HAKATON.rec");

            int i = 0;
            var timestart = DateTime.Now;

            while (i < a.Length)
            {
                byte[] part = new byte[1400];
                Buffer.BlockCopy(a, i, part, 0, 1400);
                NOP(0.000087);
                CASSubSegment newCAS = new CASSubSegment(part);
                CASSegmentManger.BufferManger(newCAS);
                i += 1400;
            }

            Console.WriteLine("***** FINISHED *****");
            var TotalSeconds = (DateTime.Now - timestart).TotalSeconds;
            Console.WriteLine($"TotalSeconds: {TotalSeconds}");
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

        private static void OnTimedEvent(object sender,
                                MicroLibrary.MicroTimerEventArgs timerEventArgs)
        {
            var systemTracks = new SystemTracks();
            systemTracks.timeStamp.seconds = 10;

            var trackData = new TrackData();
            trackData.trackID = 1;
            trackData.trackState = State.UpdateTrack;
            trackData.relativeBearing = 90;
            trackData.creationTime.day = 3;

            var trackData2 = new TrackData();
            trackData2.trackID = 2;
            trackData2.trackState = State.UpdateTrack;
            trackData2.relativeBearing = 270;
            trackData2.creationTime.day = 4;

            systemTracks.systemTracks = new List<TrackData>();
            systemTracks.systemTracks.Add(trackData);
            systemTracks.systemTracks.Add(trackData2);
            BeamMaker.onReceiveTracks(systemTracks);
        }
    }
}
