using System;
using System.Threading;

namespace LiveAudioPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LiveAudioPlayer service");

            LiveTrackBeamReciever.StartListening((liveBeam) =>
            {
                LiveAudioPlayer.LiveAudioStreamRecieved(liveBeam);
            });

            PlayerCommandsReciever.StartListening((command) =>
            {
                LiveAudioPlayer.CommandRecieved(command);
            });

            playDummyCommands();
        }

        private static void playDummyCommands()
        {
            while (true)
            {
                var c = Console.ReadLine();
                LiveAudioPlayer.CommandRecieved(c);
            }
        }
    }
}
