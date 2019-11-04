using System;
using System.Collections.Generic;
using System.Text;

namespace lucidDBManager.Data
{
    public struct TrackData
    {
        public long trackID;
        public float relativeBearing;
    }

    public class SystemTracks
    {
        public List<TrackData> systemTracks;
    }
}
