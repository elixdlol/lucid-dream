using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveAudioPlayer
{
    public static class RabbitMQConnection
    {
        static IConnection connection;

        static RabbitMQConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "172.16.20.53",
                UserName = "ferasg",
                Password = "123456",
            };

            connection = factory.CreateConnection();
        }

        internal static IConnection getConnection()
        {
            return connection;
        }

        public static void dispose()
        {
            connection.Dispose();
        }
    }
}
