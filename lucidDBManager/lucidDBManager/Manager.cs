using lucidDBManager.mongoDB;
using lucidDBManager.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace lucidDBManager
{
    public class Manager
    {
        private DataHandler Handler { get; set; }

        private RabbitMQReciever Receiver { get; set; }

        private RabbitMQSender Sender { get; set; }

        private MongoDBServer DB { get; set; }

        public Manager()
        {
            DB = new MongoDBServer();
            DB.initDB("Lucid");
            Sender = new RabbitMQSender();
            Handler = new DataHandler(Sender, DB, this);
            Receiver = new RabbitMQReciever(Handler);
        }

        public void StartReceivingUAG()
        {
            Receiver.StartRecording();
        }

        public void StopReceivingUAG()
        {
            Receiver.StopRecording();
        }
    }
}
