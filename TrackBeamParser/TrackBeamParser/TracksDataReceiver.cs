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
        static IModel trackDataChannel;

        public static void StartListening(Action<TrackData> funcThatWantTheData)
        {
            IConnection connection = RabbitMQConnection.getConnection();
            trackDataChannel = connection.CreateModel();

            trackDataChannel.ExchangeDeclare(exchange: "TrackData", type: ExchangeType.Fanout);

            trackDataChannel.QueueDeclare(queue: "track",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            trackDataChannel.QueueBind(queue: "track", exchange: "TrackData", routingKey: "");

            var consumer = new EventingBasicConsumer(trackDataChannel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                TrackData trackData = JsonConvert.DeserializeObject<TrackData>(Encoding.UTF8.GetString(body));
                funcThatWantTheData(trackData);
            };

            trackDataChannel.BasicConsume(queue: "track",
                                          autoAck: true,
                                          consumer: consumer);
        }

        public static void stopListening()
        {
            trackDataChannel.Dispose();
        }
    }
}
