using NAudio.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LiveAudioPlayer
{
    public static class LiveAudioPlayer
    {
        static int currentPlayingTrackId;
        static bool playing;
        static bool trackChanged;
        static WaveOutEvent player;
        static WaveFormat waveFormat;
        static MemoryStream stream;
        static WaveFileWriter waveFileWriter;

        static LiveAudioPlayer()
        {
            currentPlayingTrackId = -1;
            playing = false;
            trackChanged = false;
            player = new WaveOutEvent();
            waveFormat = new WaveFormat(31250, 16, 1);
            stream = new MemoryStream();
            waveFileWriter = new WaveFileWriter(new IgnoreDisposeStream(stream), waveFormat);
        }

        public static void LiveAudioStreamRecieved(TrackWithStitchedBeam liveBeam)
        {
            if (liveBeam.TrackNum == currentPlayingTrackId)
            {
                if (playing)
                {
                    writeToStream(liveBeam);

                    if (trackChanged)
                    {
                        var provider = new RawSourceWaveStream(stream, waveFormat);
                        player.Init(provider);
                        player.Play();
                        trackChanged = false;
                        Console.WriteLine($"Start playing live audio of track: {currentPlayingTrackId}");
                    }
                }
            }
        }

        private static void writeToStream(TrackWithStitchedBeam liveBeam)
        {
            var position = stream.Position;
            stream.Position = stream.Length;
            waveFileWriter.Write(liveBeam.StitchedBeam, 0, liveBeam.StitchedBeam.Length);
            waveFileWriter.Flush();
            stream.Position = position;
        }

        public static void CommandRecieved(string command)
        {
            Console.WriteLine($"Recieved command: {command}");

            if (command == "stop")
            {
                playing = false;
            }
            else
            {
                try
                {
                    int trackId = int.Parse(command);
                    playing = true;

                    if (trackId != currentPlayingTrackId)
                    {
                        trackChanged = true;
                        currentPlayingTrackId = trackId;
                        player.Stop();
                        stream = new MemoryStream();
                        waveFileWriter = new WaveFileWriter(new IgnoreDisposeStream(stream), waveFormat);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Recieved bad command...");
                }
            }
        }
    }
}
