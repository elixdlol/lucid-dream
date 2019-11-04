using lucidDBManager.Data;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace lucidDBManager.RabbitMQ
{
    public class RabbitMQReciever
    {
        ConnectionFactory Factory { get; set; }

        IConnection Connection { get; set; }

        IModel TMAChannel { get; set; }

        IModel OwnBoatChannel { get; set; }

        EventingBasicConsumer TMAConsumer { get; set; }

        EventingBasicConsumer OwnBoatConsumer { get; set; }

        DataHandler DataHandler { get; set; }

        public RabbitMQReciever(DataHandler handler)
        {
            DataHandler = handler;
            Factory = new ConnectionFactory() { HostName = "localhost" };
            Connection = Factory.CreateConnection();
            TMAChannel = Connection.CreateModel();
            OwnBoatChannel = Connection.CreateModel();
            TMAChannel.ExchangeDeclare(exchange: "TrackData", type: ExchangeType.Fanout);
            OwnBoatChannel.ExchangeDeclare(exchange: "OwnBoatData", type: ExchangeType.Fanout);

            TMAChannel.QueueDeclare("TMA");
            TMAChannel.QueueBind(queue: "TMA",
                              exchange: "TrackData",
                              routingKey: "");
            OwnBoatChannel.QueueDeclare("OwnBoat");
            OwnBoatChannel.QueueBind(queue: "OwnBoat",
                              exchange: "OwnBoatData",
                              routingKey: "");

            TMAConsumer = new EventingBasicConsumer(TMAChannel);
            TMAConsumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                TMAOriginalMessage tmaMessage = JsonConvert.DeserializeObject< TMAOriginalMessage>(message);


                DataHandler.ReceiveTMAData(tmaMessage);
            };

            OwnBoatConsumer = new EventingBasicConsumer(OwnBoatChannel);
            OwnBoatConsumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                //DataHandler.ReceiveTMAData(message);
            };

            Thread receiverTrTMA = new Thread(StartReceivingTMA);
            receiverTrTMA.Start();

            Thread receiverTrOwn = new Thread(StartReceivingOwnBoat);
            receiverTrOwn.Start();
        }

        public void StartReceivingTMA()
        {
            TMAChannel.BasicConsume(queue: "TMA",
                                autoAck: true,
                                consumer: TMAConsumer);
        }

        public void StartReceivingOwnBoat()
        {
            OwnBoatChannel.BasicConsume(queue: "OwnBoat",
                                autoAck: true,
                                consumer: OwnBoatConsumer);
        }
    }
}