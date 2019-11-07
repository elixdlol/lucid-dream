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

            Thread.Sleep(7000);
            string commandDummy = "1";
            LiveAudioPlayer.CommandRecieved(commandDummy);
            Thread.Sleep(5000);
            commandDummy = "2";
            LiveAudioPlayer.CommandRecieved(commandDummy);
            Thread.Sleep(5000);
            commandDummy = "stop";
            LiveAudioPlayer.CommandRecieved(commandDummy);
            Thread.Sleep(1000);
            commandDummy = "1";
            LiveAudioPlayer.CommandRecieved(commandDummy);
        }
    }
}
