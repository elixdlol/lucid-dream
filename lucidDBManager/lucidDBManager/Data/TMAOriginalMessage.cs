using System;
using System.Collections.Generic;
using System.Text;

namespace lucidDBManager.Data
{
    public class TMAOriginalMessage
    {
        //time stamp
        public List<OriginalSystemTrack> systemTracks;
    }

    public class OriginalSystemTrack
    {
        public long trackId;
        public long state;
        public float bearing;
        // bearing rate
        public float s_n_ratio;
        public float target_level;
        public long approach_receed_indicator;
        public long constant_bearing_warning;
        // bandwith;
        public float integration_time;
        public long integration_time_nominal;
        public long integrat_time_selection_mode;
        //time stamp
        //raw bearing candidates
    }
}
