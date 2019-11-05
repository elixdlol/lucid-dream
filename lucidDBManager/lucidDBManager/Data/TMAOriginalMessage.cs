using System;
using System.Collections.Generic;
using System.Text;
using static lucidDBManager.Data.BasicOriginalData;

namespace lucidDBManager.Data
{
    // Original system track message
    public class TMAOriginalMessage
    {
        public TimeStampType timeStamp;
        public List<OriginalSystemTrack> systemTracks;
    }
        
    public class OriginalSystemTrack
    {
        public long trackId;
        public long state;
        public float bearing;
        public IsBearingRateValidType bearingRate;
        public float s_n_ratio;
        public float target_level;
        public long approach_receed_indicator;
        public long constant_bearing_warning;
        public FilterValidType bandwidth;
        public float integration_time;
        public long integration_time_nominal;
        public long integrat_time_selection_mode;
        public TimeStampType timeStamp;
        public List<AngleValidType> rawBearingCndidates;
    }

    public class IsBearingRateValidType
    {
        public bool valid;
        public float value;
    }

    public class FilterValidType
    {
        public bool valid;
        public float lower;
        public float upper;
    }

    public class AngleValidType
    {
        public bool valid;
        public float value;
    }
}
