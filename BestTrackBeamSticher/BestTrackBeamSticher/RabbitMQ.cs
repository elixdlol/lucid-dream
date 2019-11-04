using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestTrackBeamSticher
{
    public static class RabbitMQ
    {
        static IConnection connection;

        static RabbitMQ()
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
