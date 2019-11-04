using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public static class TrackBeamDataRabbitMQSender
    {
        static IModel beamTrackDataChannel;

        static TrackBeamDataRabbitMQSender()
        {
            IConnection connection = RabbitMQConnection.getConnection();
            beamTrackDataChannel = connection.CreateModel();

            beamTrackDataChannel.ExchangeDeclare(exchange: "beamTrackData",
                                                 type: ExchangeType.Fanout,
                                                 durable: true,
                                                 autoDelete: false,
                                                 arguments: null);
        }

        public static void sendTrackBeamData(TrackBeamData trackBeamData)
        {
            byte[] body = Encoding.Default.GetBytes(JsonConvert.SerializeObject(trackBeamData));

            beamTrackDataChannel.BasicPublish(exchange: "",
                                              routingKey: "beamTrackData",
                                              basicProperties: null,
                                              body: body);
        }

        public static void dispose()
        {
            beamTrackDataChannel.Dispose();
        }
    }
}
