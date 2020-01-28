using System;
using System.Collections.Generic;
using System.Text;

namespace lucidDBManager.Data
{
    class IPSOriginalMessage
    {
        public long count;
        public long last_pulse;
        public long freeze;
        public List<IPSTrack> tracks;
        public PingSteal ping_steal;


        public class IPSTrack
        {

        }

        public class PingSteal
        {
            public long track_identification;
            public float time_distance;
            public float reflection_depth;
            public float own_boat_depth;
            public float sound_velocity;
            public FloatRealValidType target_range;
        }
        public class FloatRealValidType
        {
            public bool valid;
            public float value;
        }
    }
}
