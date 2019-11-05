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

        public static void StartListening(Action<SystemTracks> funcThatWantTheData)
        {
            IConnection connection = RabbitMQConnection.getConnection();
            trackDataChannel = connection.CreateModel();

            trackDataChannel.ExchangeDeclare(exchange: "LucidTrackData", type: ExchangeType.Fanout);

            trackDataChannel.QueueDeclare(queue: "track",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: true,
                                 arguments: null);

            trackDataChannel.QueueBind(queue: "track", exchange: "LucidTrackData", routingKey: "");

            var consumer = new EventingBasicConsumer(trackDataChannel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                SystemTracks trackData = JsonConvert.DeserializeObject<SystemTracks>(Encoding.UTF8.GetString(body));
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
