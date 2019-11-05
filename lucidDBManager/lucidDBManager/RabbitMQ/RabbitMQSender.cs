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
            Channel.ExchangeDeclare(exchange: "LucidTrackData", type: ExchangeType.Fanout);
            Channel.ExchangeDeclare(exchange: "LucidOwnBoatData", type: ExchangeType.Fanout);
        }

        public void SendTrackData(SystemTracks message)
        {
            string json = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(json);

            Channel.BasicPublish(exchange: "LucidTrackData",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }

        public void SendOwnBoatData(OwnBoatData message)
        {
            string json = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(json);

            Channel.BasicPublish(exchange: "LucidOwnBoatData",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
