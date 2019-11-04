using BestTrackBeamStitcher;
using System;

namespace BestTrackBeamSticher
{
    class Program
    {
        static void Main(string[] args)
        {
            TrackBeamDataReciever.StartListening((trackBeamData) =>
            {
                TrackWithStitchedBeam trackWithStitchedBeam = Stitcher.stitch(trackBeamData);
                TrackWithStitchedBeamSender.sendTrackWithStitchedBeam(trackWithStitchedBeam);
            });
        }
    }
}
