using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public static class BeamMaker
    {
        public static void onReceiveTracks(SystemTracks trackData)
        {
            //TODO
            double heading = 0;// = Parser.GetHeading();

            //retreive the actual beams from the BeamBuffer
            byte[][] beamArray = BeamsBuffer.getBeamsAndFlush();

            foreach (var track in trackData.systemTracks)
            {
                TrackBeamData trackBeamData = CalcBeams(track.trackID, heading, track.relativeBearing, beamArray);
                TrackBeamDataSender.sendTrackBeamData(trackBeamData);
            }
        }

        public static TrackBeamData CalcBeams(long trackNum, double heading, double RB, byte[][] beamArray)
        {
            var trackBeamData = new TrackBeamData();
            trackBeamData.TrackNum = (int)trackNum;

            double trackDegree = (heading + RB) % 360;
            const double factor = 192 / 360;
            double beamNumber = trackDegree * factor;

            int beamNum1 = (int)(Math.Floor(beamNumber));
            int beamNum2 = (int)(Math.Ceiling(beamNumber)); 
            double precentage = Math.Abs(beamNum2 - beamNumber);

            // TODO: return the first 2 beams for now..
            trackBeamData.Beam1 = beamArray[0];
            trackBeamData.Beam2 = beamArray[1];
            //trackBeamData.Beam1 = beamArray[beamNum1];
            //trackBeamData.Beam2 = beamArray[beamNum2];
            trackBeamData.Precentage = precentage;

            return trackBeamData;
        }
    }
}
