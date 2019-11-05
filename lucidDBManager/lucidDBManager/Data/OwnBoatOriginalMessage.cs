using System;
using System.Collections.Generic;
using System.Text;
using static lucidDBManager.Data.BasicOriginalData;

namespace lucidDBManager.Data
{
    public class OwnBoatOriginalMessage
    {
        public IdlHeader idlHeader = new IdlHeader();
        public SystemTime systemTime = new SystemTime();
        public Timezone timeZone = new Timezone();
        public Heading heading = new Heading();
        public HeadingRate heading_rate = new HeadingRate();
        public Roll roll = new Roll();
        public RollRate roll_rate = new RollRate();
        public Pitch pitch = new Pitch();
        public PitchRate pitch_rate = new PitchRate();
        public Heave heave = new Heave();
        public HeaveRate heave_rate = new HeaveRate();
        public CourseOverGround course_overe_ground = new CourseOverGround();
    }

    public class IdlHeader
    {
        public long message_state;
        public long message_source;
        public TimeStampType compile_time_of_message = new TimeStampType();
        public long number_of_bytes;
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
