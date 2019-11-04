using System;
using lucidDBManager.Data;
using lucidDBManager.RabbitMQ;
using lucidDBManager.mongoDB;

namespace lucidDBManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongoDB = new MongoDBServer();
            mongoDB.initDB("Lucid");
            var sender = new RabbitMQSender();
            var dataHandler = new DataHandler(sender,mongoDB);
            var receiver = new RabbitMQReciever(dataHandler);

            //TrackData data = new TrackData() { trackID = 1, relativeBearing = (float)0.05 };

            //sender.sendTrackData(data);
        }
    }
}
