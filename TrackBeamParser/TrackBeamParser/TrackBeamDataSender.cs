using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public static class TrackBeamDataSender
    {
        static IModel beamTrackDataChannel;

        static TrackBeamDataSender()
        {
            IConnection connection = RabbitMQConnection.getConnection();
            beamTrackDataChannel = connection.CreateModel();

            beamTrackDataChannel.ExchangeDeclare(exchange: "beamTrackData",
                                                 type: ExchangeType.Fanout);
        }

        public static void sendTrackBeamData(TrackBeamData trackBeamData)
        {
            byte[] body = Encoding.Default.GetBytes(JsonConvert.SerializeObject(trackBeamData));

            beamTrackDataChannel.BasicPublish(exchange: "beamTrackData",
                                              routingKey: "",
                                              basicProperties: null,
                                              body: body);
        }

        public static void dispose()
        {
            beamTrackDataChannel.Dispose();
        }
    }
}
