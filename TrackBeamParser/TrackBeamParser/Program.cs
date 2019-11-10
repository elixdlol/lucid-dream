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
        static int i = 0;
        static byte[] FileBytes = File.ReadAllBytes(@"C:\Users\96ron\Desktop\האקויק ראגב\CAS_HAKATON.rec");
        static MicroLibrary.MicroTimer AudioMicroTimer;
        static DateTime timestart;

        static void Main(string[] args)
        {
            Console.WriteLine("TrackBeamParser service");

            Thread thread = new Thread(() =>
            {
                TracksDataReceiver.StartListening((trackData) =>
                {
                    BeamMaker.onReceiveTracks(trackData);
                });
            });
            thread.Start();

            timestart = DateTime.Now;

            AudioMicroTimer = new MicroLibrary.MicroTimer();
            AudioMicroTimer.MicroTimerElapsed +=
                new MicroLibrary.MicroTimer.MicroTimerElapsedEventHandler(OnTimedEventAudio);

            AudioMicroTimer.Interval = 100; // 1000µs = 1ms
            AudioMicroTimer.Enabled = true; // Start timer
        }


        private static void OnTimedEventAudio(object sender,
                                MicroLibrary.MicroTimerEventArgs timerEventArgs)
        {
            if (i >= FileBytes.Length)
            {
                Console.WriteLine("***** FINISHED *****");
                var TotalSeconds = (DateTime.Now - timestart).TotalSeconds;
                Console.WriteLine($"TotalSeconds: {TotalSeconds}");
                AudioMicroTimer.Enabled = false;
                return;
            }

            byte[] part = new byte[1400];
            Buffer.BlockCopy(FileBytes, i, part, 0, 1400);
            CASSubSegment newCAS = new CASSubSegment(part);
            CASSegmentManger.BufferManger(newCAS);
            i += 1400;
        }

        private static void NOP(double durationSeconds)
        {
            var durationTicks = Math.Round(durationSeconds * Stopwatch.Frequency);
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedTicks < durationTicks)
            {

            }
        }
    }
}
