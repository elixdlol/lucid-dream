using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackBeamParser
{
    public static class RabbitMQConnection
    {
        static IConnection connection;

        static RabbitMQConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "172.16.20.161",
                UserName = "rutush",
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
