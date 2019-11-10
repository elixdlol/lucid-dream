using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LucidDreamSystem
{
    class RabbitMQSender
    {
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        public RabbitMQSender()
        {
            factory = new ConnectionFactory()
            {
                HostName = "localhost"
                //HostName = "172.16.20.161",
                //UserName = "rutush",
                //Password = "123456"
            };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "TrackData", type: ExchangeType.Fanout);
        }
    
        public void SendData(string data)
        {   
                var body = Encoding.UTF8.GetBytes(data);
                channel.BasicPublish(exchange: "TrackData",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", data);
              
        }
    }
}
