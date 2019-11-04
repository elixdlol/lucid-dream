using System;

namespace BestTrackBeamSticher
{
    class Program
    {
        static void Main(string[] args)
        {
            TrackBeamDataReciever.StartListening(funcThatWantTheData);
        }

        static void funcThatWantTheData(TrackBeamData data)
        {
            Console.WriteLine($"RONEN WAS HERE, trackNum:{data.TrackNum} beam1:{data.Beam1} beam2:{data.Beam2} prec:{data.Precentage}");
        }
    }
}
