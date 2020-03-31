using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GlobalResources.BasicData;


namespace BdtCasMessage
{
   public class OriginalBdtCasMessage
    {
        public TimeType timeStamp;
        public List<TrackData> systemTracks;

        public OriginalBdtCasMessage()
        {
            GetGenerateObject();
        }

        public OriginalBdtCasMessage(TimeType timeStamp, List<TrackData> systemTracks)
        {
            this.timeStamp = new TimeType();
            this.timeStamp.c_seconds = timeStamp.c_seconds;
            this.timeStamp.seconds = timeStamp.seconds;
            this.timeStamp.minutes = timeStamp.minutes;
            this.timeStamp.hours = timeStamp.hours;
            this.timeStamp.day = timeStamp.day;
            this.timeStamp.month = timeStamp.month;
            this.timeStamp.year = timeStamp.year;

            this.systemTracks = new List<TrackData>();

            for(int i = 0; i < systemTracks.Count; i++)
            {
                TrackData trackData = new TrackData();
                trackData.trackID = systemTracks[i].trackID;
                trackData.trackState = systemTracks[i].trackState;

                trackData.creationTime = new TimeType();
                trackData.creationTime.c_seconds = timeStamp.c_seconds;
                trackData.creationTime.seconds = timeStamp.seconds;
                trackData.creationTime.minutes = timeStamp.minutes;
                trackData.creationTime.hours = timeStamp.hours;
                trackData.creationTime.day = timeStamp.day;
                trackData.creationTime.month = timeStamp.month;
                trackData.creationTime.year = timeStamp.year;

                trackData.relativeBearing = systemTracks[i].relativeBearing;
                trackData.relativeBearingRate = systemTracks[i].relativeBearingRate;
                this.systemTracks.Add(trackData);
            }
        }
        private OriginalBdtCasMessage GetGenerateObject()
        {
            this.timeStamp = new TimeType();
            this.timeStamp.c_seconds = 1;
            this.timeStamp.seconds = 1;
            this.timeStamp.minutes = 1;
            this.timeStamp.hours = 1;
            this.timeStamp.day = 1;
            this.timeStamp.month = 1;
            this.timeStamp.year = 1;

            this.systemTracks = new List<TrackData>();
            var trackData = new TrackData();
            trackData.trackID = 1;
            trackData.trackState = State.NewTrack;

            trackData.creationTime = new TimeType();
            trackData.creationTime.c_seconds = 1;
            trackData.creationTime.seconds = 1;
            trackData.creationTime.minutes = 1;
            trackData.creationTime.hours = 1;
            trackData.creationTime.day = 1;
            trackData.creationTime.month = 1;
            trackData.creationTime.year = 1;

            trackData.relativeBearing = 1;
            trackData.relativeBearingRate = 1;
            this.systemTracks.Add(trackData);
            return new OriginalBdtCasMessage(this.timeStamp, this.systemTracks);
        }
    }

    public struct TrackData
    {
        public long trackID;
        public State trackState;
        public TimeType creationTime;
        public float relativeBearing;
        public float relativeBearingRate;
    }
    public enum State
    {
        NewTrack,
        UpdateTrack,
        DeleteTrack
    }

    
}
