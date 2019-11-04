using BestTrackBeamSticher;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestTrackBeamStitcher
{
    public static class Stitcher
    {
        public static TrackWithStitchedBeam stitch(TrackBeamData trackBeamData)
        {
            TrackWithStitchedBeam trackWithStitchedBeam = new TrackWithStitchedBeam();
            trackWithStitchedBeam.TrackNum = trackBeamData.TrackNum;

            byte[] beam1 = trackBeamData.Beam1;
            byte[] beam2 = trackBeamData.Beam2;
            double beam1Precentage = trackBeamData.Precentage;
            double beam2Precentage = 1- trackBeamData.Precentage;

            setVolume(beam1, beam1Precentage);
            setVolume(beam2, beam2Precentage);

            byte[] stitchedBeam = stitchBeams(beam1, beam2);
            trackWithStitchedBeam.StitchedBeam = stitchedBeam;

            return trackWithStitchedBeam;
        }

        static private void setVolume(byte[] buffer, double volume)
        {
            // scaling volume of buffer audio
            for (int i = 0; i < buffer.Length / 2; ++i)
            {
                // convert to 16-bit
                short sample = (short)((buffer[i * 2 + 1] << 8) | buffer[i * 2]);

                // scale
                double gain = volume; // value between 0 and 1.0
                sample = (short)(sample * gain + 0.5);

                // back to byte[]
                buffer[i * 2 + 1] = (byte)(sample >> 8);
                buffer[i * 2] = (byte)(sample & 0xff);
            }
        }

        static private byte[] stitchBeams(byte[] buffer, byte[] buffer2)
        {
            byte[] mixedBuffer = new byte[buffer.Length];

            for (int i = 0; i < buffer.Length; ++i)
            {
                mixedBuffer[i] = (byte)(buffer[i] + buffer2[i]);
            }

            return mixedBuffer;
        }
    }
}
