using System;
using lucidDBManager.Data;
using lucidDBManager.RabbitMQ;

namespace lucidDBManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var sender = new RabbitMQSender();
            var dataHandler = new DataHandler(sender);
            var receiver = new RabbitMQReciever(dataHandler);

            //TrackData data = new TrackData() { trackID = 1, relativeBearing = (float)0.05 };

            //sender.sendTrackData(data);
        }
    }
}
