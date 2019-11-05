using System;
using System.Collections.Generic;
using System.Text;

namespace lucidDBManager.Data
{
    public class BasicOriginalData
    {
        public class TimeStampType
        {
            public HmssType time;
            public YmdType date;
        }

        public class HmssType
        {
            public long hours;
            public long minutes;
            public long seconds;
            public long c_seconds;
        }

        public class YmdType
        {
            public long year;
            public long month;
            public long day;
        }
    }
}
