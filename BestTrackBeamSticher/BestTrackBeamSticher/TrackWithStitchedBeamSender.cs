using BestTrackBeamSticher;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestTrackBeamStitcher
{
    public static class TrackWithStitchedBeamSender
    {
        static IModel TrackWithStitchedBeamChannel;

        static TrackWithStitchedBeamSender()
        {
            IConnection connection = RabbitMQConnection.getConnection();
            TrackWithStitchedBeamChannel = connection.CreateModel();

            TrackWithStitchedBeamChannel.ExchangeDeclare(exchange: "trackWithStitchedBeamData",
                                                 type: ExchangeType.Fanout,
                                                 durable: true,
                                                 autoDelete: false,
                                                 arguments: null);
        }

        public static void sendTrackWithStitchedBeam(TrackWithStitchedBeam trackWithStitchedBeam)
        {
            byte[] body = Encoding.Default.GetBytes(JsonConvert.SerializeObject(trackWithStitchedBeam));

            TrackWithStitchedBeamChannel.BasicPublish(exchange: "",
                                              routingKey: "trackWithStitchedBeamData",
                                              basicProperties: null,
                                              body: body);
        }

        public static void dispose()
        {
            TrackWithStitchedBeamChannel.Dispose();
        }
    }
}
