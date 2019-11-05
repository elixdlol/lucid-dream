using System;
using System.Collections.Generic;
using System.Text;
using static lucidDBManager.Data.BasicData;

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

    

    public enum State
    {
        NewTrack,
        UpdateTrack,
        DeleteTrack
    }

}
