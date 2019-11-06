using BestTrackBeamStitcher;
using NAudio.Utils;
using NAudio.Wave;
using System;
using System.IO;

namespace BestTrackBeamSticher
{
    class Program
    {
        static WaveFileWriter waveFileWriter;

        static void Main(string[] args)
        {
            TrackBeamDataReciever.StartListening((trackBeamData) =>
            {
                TrackWithStitchedBeam trackWithStitchedBeam = Stitcher.stitch(trackBeamData);
                TrackWithStitchedBeamSender.sendTrackWithStitchedBeam(trackWithStitchedBeam);

                byte[] wavBytes = trackWithStitchedBeam.StitchedBeam;
                var file = $@"C:\Users\96ron\Desktop\האקויק ראגב\ronen44.wav";
                var waveFormat = new WaveFormat(31250, 16, 1);

                if (waveFileWriter == null)
                {
                    waveFileWriter = new WaveFileWriter(file, waveFormat);
                }
                waveFileWriter.Write(wavBytes, 0, wavBytes.Length);
                waveFileWriter.Flush();
            });
        }
    }
}
