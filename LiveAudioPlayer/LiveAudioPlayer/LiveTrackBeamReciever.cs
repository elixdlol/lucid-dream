using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveAudioPlayer
{
    public static class LiveTrackBeamReciever
    {
        static IModel liveBeamChannel; 

        public static void StartListening(Action<TrackWithStitchedBeam> funcThatWantTheData)
        {
            IConnection connection = RabbitMQConnection.getConnection();
            liveBeamChannel = connection.CreateModel();

            liveBeamChannel.ExchangeDeclare(exchange: "trackWithStitchedBeamData",
                                            type: ExchangeType.Fanout,
                                            durable: true,
                                            autoDelete: false,
                                            arguments: null);

            liveBeamChannel.QueueDeclare(queue: "liveBeamData",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            liveBeamChannel.QueueBind(queue: "liveBeamData", exchange: "trackWithStitchedBeamData", routingKey: "");

            var consumer = new EventingBasicConsumer(liveBeamChannel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                TrackWithStitchedBeam liveBeam = JsonConvert.DeserializeObject<TrackWithStitchedBeam>(Encoding.UTF8.GetString(body));
                funcThatWantTheData(liveBeam);
            };

            liveBeamChannel.BasicConsume(queue: "liveBeamData",
                                          autoAck: true,
                                          consumer: consumer);
        }

        public static void stopListening()
        {
            liveBeamChannel.Dispose();
        }
    }
}
