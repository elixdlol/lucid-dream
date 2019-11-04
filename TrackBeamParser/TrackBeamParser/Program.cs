using System;

namespace TrackBeamParser
{
    class Program
    {
        static void Main(string[] args)
        {
            TracksDataReceiver.StartListening(funcThatWantTheData);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        static void funcThatWantTheData(TrackData data)
        {
            Console.WriteLine($"RONEN WAS HERE, track number: {data.trackID}, track angle: {data.relativeBearing}");
        }
    }
}
