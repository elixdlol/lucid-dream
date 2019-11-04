using System;
using System.Collections.Generic;
using System.Text;

namespace lucidDBManager.Data
{
    public struct TrackData
    {
        public long trackID;
        public TimeStampType creationTime;
        public float relativeBearing;
        public float relativeBearingRate;
    }

    public class SystemTracks
    {
        public TimeStampType timeStamp;
        public List<TrackData> systemTracks;
    }

}
