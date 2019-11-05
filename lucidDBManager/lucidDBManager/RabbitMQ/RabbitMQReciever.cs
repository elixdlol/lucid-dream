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
            TMAChannel.ExchangeDeclare(exchange: "TrackData", type: ExchangeType.Fanout);
            TMAChannel.QueueDeclare("UAGTrackDataQueue");
            TMAChannel.QueueBind(queue: "UAGTrackDataQueue",
                              exchange: "TrackData",
                              routingKey: "");            

            TMAConsumer = new EventingBasicConsumer(TMAChannel);
            TMAConsumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                TMAOriginalMessage tmaMessage = JsonConvert.DeserializeObject< TMAOriginalMessage>(message);


                DataHandler.ReceiveTMAData(tmaMessage);
            };

            TMAChannel.BasicConsume(queue: "UAGTrackDataQueue",
                                autoAck: true,
                                consumer: TMAConsumer);


            OwnBoatChannel = Connection.CreateModel();
            OwnBoatChannel.ExchangeDeclare(exchange: "OwnBoatData", type: ExchangeType.Fanout);
            OwnBoatChannel.QueueDeclare("UAGOwnBoatQueue");
            OwnBoatChannel.QueueBind(queue: "UAGOwnBoatQueue",
                              exchange: "OwnBoatData",
                              routingKey: "");
            OwnBoatConsumer = new EventingBasicConsumer(OwnBoatChannel);
            OwnBoatConsumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                //DataHandler.ReceiveTMAData(message);
            };


            OwnBoatChannel.BasicConsume(queue: "UAGOwnBoatQueue",
                                autoAck: true,
                                consumer: OwnBoatConsumer);
        }

    }
}