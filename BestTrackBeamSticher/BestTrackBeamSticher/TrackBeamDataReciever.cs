using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestTrackBeamSticher
{
    public static class TrackBeamDataReciever
    {
        static IModel trackDataChannel;

        public static void StartListening(Action<TrackBeamData> funcThatWantTheData)
        {
            IConnection connection = RabbitMQConnection.getConnection();
            trackDataChannel = connection.CreateModel();

            trackDataChannel.ExchangeDeclare(exchange: "beamTrackData",
                                        type: ExchangeType.Fanout);

            trackDataChannel.QueueDeclare(queue: "beamTrack",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: true,
                                 arguments: null);

            trackDataChannel.QueueBind(queue: "beamTrack", exchange: "beamTrackData", routingKey: "");

            var consumer = new EventingBasicConsumer(trackDataChannel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                TrackBeamData trackData = JsonConvert.DeserializeObject<TrackBeamData>(Encoding.UTF8.GetString(body));
                funcThatWantTheData(trackData);
            };

            trackDataChannel.BasicConsume(queue: "beamTrack",
                                          autoAck: true,
                                          consumer: consumer);
        }

        public static void stopListening()
        {
            trackDataChannel.Dispose();
        }
    }
}
