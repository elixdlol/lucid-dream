using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public class TracksDataReceiver
    {
        static IConnection connection;
        static IModel channel;

        private TracksDataReceiver()
        {
        }

        public static void StartListening(Action<TrackData> funcThatWantTheData)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "172.16.20.161",
                UserName = "rutush",
                Password = "123456",
            };

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "TrackData", type: ExchangeType.Fanout);

            channel.QueueDeclare(queue: "track",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.QueueBind(queue: "track", exchange: "TrackData", routingKey: "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                TrackData trackData = JsonConvert.DeserializeObject<TrackData>(Encoding.UTF8.GetString(body));
                funcThatWantTheData(trackData);
            };

            channel.BasicConsume(queue: "track",
                                 autoAck: true,
                                 consumer: consumer);
        }

        public static void StopLintening()
        {
            channel.Dispose();
            connection.Dispose();
        }
    }
}
