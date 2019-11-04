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

        IModel Channel { get; set; }

        EventingBasicConsumer Consumer { get; set; }

        public RabbitMQReciever()
        {
            Factory = new ConnectionFactory() { HostName = "localhost" };
            Connection = Factory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(exchange: "TMA", type: ExchangeType.Fanout);
            
            Channel.QueueDeclare("TMA");
            Channel.QueueBind(queue: "TMA",
                              exchange: "TrackData",
                              routingKey: "");

            Consumer = new EventingBasicConsumer(Channel);
            Consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
            };

            Thread receiverTr = new Thread(StartReceiving);
            receiverTr.Start();
        }

        public void StartReceiving()
        {
            Channel.BasicConsume(queue: "TMA",
                                autoAck: true,
                                consumer: Consumer);
        }



    }
}