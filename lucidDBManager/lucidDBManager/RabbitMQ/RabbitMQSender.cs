using lucidDBManager.Data;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace lucidDBManager.RabbitMQ
{
    public class RabbitMQSender
    {
        ConnectionFactory Factory { get; set; }

        IConnection Connection { get; set; }

        IModel Channel { get; set; }

        public RabbitMQSender()
        {
            Factory = new ConnectionFactory() { HostName = "localhost" };
            Connection = Factory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(exchange: "TrackData", type: ExchangeType.Fanout);
        }

        public void sendTrackData(SystemTracks message)
        {
            string json = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(json);

            Channel.BasicPublish(exchange: "TrackData",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
