using System;
using System.Collections.Generic;
using System.Text;

namespace lucidDBManager.Data
{
    public struct TrackData
    {
        public long trackID;
        public State trackState;
        public TimeType creationTime;
        public float relativeBearing;
        public float relativeBearingRate;
    }

    public class SystemTracks
    {
        public TimeType timeStamp;
        public List<TrackData> systemTracks;
    }

    public struct TimeType
    {
        public long hours;
        public long minutes;
        public long seconds;
        public long c_seconds;
        public long year;
        public long month;
        public long day;
    }

    public enum State
    {
        NewTrack,
        UpdateTrack,
        DeleteTrack
    }

}
