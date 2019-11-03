using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public class BeamMaker
    {
        public void onReceiveTracks(/*List<TrackData>*/)
        {
            //TODO
            double heading = 0;// = Parser.GetHeading();
            int[] tracks = { };

            //retreive the actual beams from the BeamBuffer
            byte[][] beamArray = BeamsBuffer.getBeamsAndFlush();

            foreach (var track in tracks)
            {
                TrackBeamData trackBeamData = CalcBeams(0, heading, 0.0, beamArray);
                // send via rabbitmq
            }
        }

        public TrackBeamData CalcBeams(int trackNum, double heading, double RB, byte[][] beamArray)
        {
            var trackBeamData = new TrackBeamData();
            trackBeamData.TrackNum = trackNum;

            //voodoo
            //calc beam numbers and precentage from heading and rb

            double trackDegree = (heading + RB) % 360;
            const double factor = 192 / 360;
            double beamNumber = trackDegree * factor;

            int beamNum1 = (int)(Math.Floor(beamNumber));
            int beamNum2 = (int)(Math.Ceiling(beamNumber)); 
            double precentage = Math.Abs(beamNum2 - beamNumber);

            
            trackBeamData.Beam1 = beamArray[beamNum1];
            trackBeamData.Beam2 = beamArray[beamNum2];
            trackBeamData.Precentage = precentage;

            return trackBeamData;
        }
    }
}
