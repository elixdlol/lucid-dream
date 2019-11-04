using System;
using lucidDBManager.Data;
using lucidDBManager.RabbitMQ;

namespace lucidDBManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var receiver = new RabbitMQReciever();
            var sender = new RabbitMQSender();

            //TrackData data = new TrackData() { trackID = 1, relativeBearing = (float)0.05 };

            //sender.sendTrackData(data);
        }
    }
}
