using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveAudioPlayer
{
    public static class PlayerCommandsReciever
    {
        static IModel playerCommandsChannel;

        public static void StartListening(Action<string> funcThatWantTheData)
        {
            IConnection connection = RabbitMQConnection.getConnection();
            playerCommandsChannel = connection.CreateModel();

            playerCommandsChannel.ExchangeDeclare(exchange: "liveAudioPlayerCommands",
                                            type: ExchangeType.Fanout,
                                            durable: true,
                                            autoDelete: false,
                                            arguments: null);

            playerCommandsChannel.QueueDeclare(queue: "commandQueue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            playerCommandsChannel.QueueBind(queue: "commandQueue", exchange: "liveAudioPlayerCommands", routingKey: "");

            var consumer = new EventingBasicConsumer(playerCommandsChannel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                string command = Encoding.UTF8.GetString(body);
                funcThatWantTheData(command);
            };

            playerCommandsChannel.BasicConsume(queue: "commandQueue",
                                          autoAck: true,
                                          consumer: consumer);
        }

        public static void stopListening()
        {
            playerCommandsChannel.Dispose();
        }
    }
}
