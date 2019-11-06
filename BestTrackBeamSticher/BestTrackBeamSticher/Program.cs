using BestTrackBeamStitcher;
using NAudio.Utils;
using NAudio.Wave;
using System;
using System.IO;

namespace BestTrackBeamSticher
{
    class Program
    {
        static MemoryStream ms = new MemoryStream();
        static WaveFormat waveFormat = new WaveFormat(31250, 16, 1);
        static WaveFileWriter waveFileWriter = new WaveFileWriter(new IgnoreDisposeStream(ms), waveFormat);
        static int i = 0;

        static void Main(string[] args)
        {
            TrackBeamDataReciever.StartListening((trackBeamData) =>
            {
                TrackWithStitchedBeam trackWithStitchedBeam = Stitcher.stitch(trackBeamData);
                TrackWithStitchedBeamSender.sendTrackWithStitchedBeam(trackWithStitchedBeam);

                playFirstTrackAudio(trackWithStitchedBeam);
            });
        }

        static void playFirstTrackAudio(TrackWithStitchedBeam trackWithStitchedBeam)
        {
            if (trackWithStitchedBeam.TrackNum == 2)
            {
                var t = ms.Position;
                ms.Position = ms.Length;
                waveFileWriter.Write(trackWithStitchedBeam.StitchedBeam, 0, trackWithStitchedBeam.StitchedBeam.Length);
                waveFileWriter.Flush();
                ms.Position = t;

                if (++i == 1)
                {
                    var provider = new RawSourceWaveStream(ms, waveFormat);
                    var player = new WaveOutEvent();
                    player.Init(provider);
                    player.Play();
                    Console.WriteLine("Playing");
                }
            }
        }
    }
}
