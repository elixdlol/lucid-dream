using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucidDreamSystem
{
    class OwnBoat_OriginalMessage
    {
        IdlHeader idlHeader = new IdlHeader();
        SystemTime systemTime = new SystemTime();
        Heading heading = new Heading();
        HeadingRate heading_rate = new HeadingRate();
        Roll roll = new Roll();
        RollRate roll_rate = new RollRate();
        Pitch pitch = new Pitch();
        PitchRate pitch_rate = new PitchRate();
        Heave heave = new Heave();
        HeaveRate heave_rate = new HeaveRate();
        CourseOverGround course_overe_ground = new CourseOverGround();

    }
    public class IdlHeader
    {
        public long message_state;
        public long message_source;
        public TimeStampType compile_time_of_message = new TimeStampType();
        public long number_of_bytes;
    }

    public class TimeStampType
    {
        public HmssType time = new HmssType();
        public YmdType date = new YmdType();
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

    public class SystemTime
    {
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();
    }
    public class TimeStampValidType
    {
        public bool valid;
        public TimeStampType value = new TimeStampType();
    }
    public class Timezone
    {
        public LongRealValidType data = new LongRealValidType();
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();

    }
    public class LongRealValidType
    {
        public bool valid;
        public double value;
    }
    public class Heading
    {
        public LongRealValidType data = new LongRealValidType();
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();
    }
    public class HeadingRate
    {
        public LongRealValidType data = new LongRealValidType();
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();
    }
    public class Roll
    {
        public LongRealValidType data = new LongRealValidType();
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();
    }
    public class RollRate
    {
        public LongRealValidType data = new LongRealValidType();
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();
    }
    public class Pitch
    {
        public LongRealValidType data = new LongRealValidType();
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();
    }
    public class PitchRate
    {
        public LongRealValidType data = new LongRealValidType();
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();
    }
    public class Heave
    {
        public LongRealValidType data = new LongRealValidType();
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();
    }
    public class HeaveRate
    {
        public LongRealValidType data = new LongRealValidType();
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();
    }
    public class CourseOverGround
    {
        public LongRealValidType data = new LongRealValidType();
        public bool is_current;
        public long sensor;
        public TimeStampValidType time = new TimeStampValidType();
    }
}
