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
        ConnectionFactory factory = new ConnectionFactory();

        public RabbitMQSender()
        {
            factory.HostName = "172.16.20.161";
            factory.UserName = "rutush";
            factory.Password = "123456";
            
        }
        public void send_data(string data)
        {

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "OwnBoatData", type: ExchangeType.Fanout);

                
                var body = Encoding.UTF8.GetBytes(data);
                channel.BasicPublish(exchange: "OwnBoatData",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", data);
               

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }



    }
}
