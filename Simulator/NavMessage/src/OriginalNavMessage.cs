using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GlobalResources.BasicData;

namespace NavMessage
{
    public class OriginalNavMessage
    {
        public TimeType timeStamp;
        public int timeZone;
        public float heading;
        public float headingRate;
        public float roll;
        public float rollRate;
        public float pitch;
        public float pitchRate;
        public float heave;
        public float heaveRate;
        public float course_overe_ground;

        public OriginalNavMessage()
        {
            GetGeneratedObject();
        }

        public OriginalNavMessage(TimeType timeStamp, int timeZone, float heading, float headingRate, float roll, float rollRate, float pitch, float pitchRate, float heave, float heaveRate,float course_overe_ground)
        {
            this.timeStamp = new TimeType();
            this.timeStamp.c_seconds = timeStamp.c_seconds;
            this.timeStamp.seconds = timeStamp.seconds;
            this.timeStamp.minutes = timeStamp.minutes;
            this.timeStamp.hours = timeStamp.hours;
            this.timeStamp.day = timeStamp.day;
            this.timeStamp.month = timeStamp.month;
            this.timeStamp.year = timeStamp.year;

            this.timeZone = timeZone;
            this.heading = heading;
            this.headingRate = headingRate;
            this.roll = roll;
            this.rollRate = rollRate;
            this.pitch = pitch;
            this.pitchRate = pitchRate;
            this.heave = heave;
            this.heaveRate = heaveRate;
            this.course_overe_ground = course_overe_ground;

            
        }
        private OriginalNavMessage GetGeneratedObject()
        {
            this.timeStamp = new TimeType();
            timeStamp.c_seconds = 1;
            timeStamp.seconds = 1;
            timeStamp.minutes = 1;
            timeStamp.hours = 1;
            timeStamp.day = 1;
            timeStamp.month = 1;
            timeStamp.year = 1;

            return new OriginalNavMessage(this.timeStamp, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1);
        }
    }

   

    
}
