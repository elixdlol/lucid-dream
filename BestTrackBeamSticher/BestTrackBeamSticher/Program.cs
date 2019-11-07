using BestTrackBeamStitcher;
using NAudio.Utils;
using NAudio.Wave;
using System;
using System.IO;

namespace BestTrackBeamSticher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BestTrackBeamSticher service");

            TrackBeamDataReciever.StartListening((trackBeamData) =>
            {
                TrackWithStitchedBeam trackWithStitchedBeam = Stitcher.stitch(trackBeamData);
                TrackWithStitchedBeamSender.sendTrackWithStitchedBeam(trackWithStitchedBeam);
            }); 
        }
    }
}
