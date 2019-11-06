using System;
using System.Collections.Generic;
using System.Text;
using static lucidDBManager.Data.BasicData;

namespace lucidDBManager.Data
{
    public class OwnBoatData
    {
        public TimeType timeStamp;
        public Timezone timeZone = new Timezone();
        public Heading heading = new Heading();
        public Roll roll = new Roll();
        public Pitch pitch = new Pitch();
        public Heave heave = new Heave();
        public CourseOverGround course_overe_ground = new CourseOverGround();
    }

    public class OwnBoatHeading
    {
        public ValidLongRealType data = new ValidLongRealType();
        public bool is_current;
        public long sensor;
        public ValidTimeStampType time = new ValidTimeStampType();
    }
    public class OwnBoatRoll
    {
        public ValidLongRealType data = new ValidLongRealType();
        public bool is_current;
        public long sensor;
        public ValidTimeStampType time = new ValidTimeStampType();
    }
    public class OwnBoatPitch
    {
        public ValidLongRealType data = new ValidLongRealType();
        public bool is_current;
        public long sensor;
        public ValidTimeStampType time = new ValidTimeStampType();
    }
    public class OwnBoatHeave
    {
        public ValidLongRealType data = new ValidLongRealType();
        public bool is_current;
        public long sensor;
        public ValidTimeStampType time = new ValidTimeStampType();
    }
    public class OwnBoatCourseOverGround
    {
        public ValidLongRealType data = new ValidLongRealType();
        public bool is_current;
        public long sensor;
        public ValidTimeStampType time = new ValidTimeStampType();
    }

    public class ValidTimeStampType
    {
        public bool valid;
        public TimeType value = new TimeType();
    }

    public class ValidLongRealType
    {
        public bool valid;
        public double value;
    }
}
