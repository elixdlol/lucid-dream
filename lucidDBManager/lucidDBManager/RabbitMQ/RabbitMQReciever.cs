using lucidDBManager.Data;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace lucidDBManager.RabbitMQ
{
    public class RabbitMQReciever
    {
        ConnectionFactory Factory { get; set; }

        IConnection Connection { get; set; }

        IModel Channel { get; set; }
        
        EventingBasicConsumer TMAConsumer { get; set; }

        EventingBasicConsumer OwnBoatConsumer { get; set; }

        EventingBasicConsumer ActionConsumer { get; set; }

        DataHandler DataHandler { get; set; }

        public RabbitMQReciever(DataHandler handler)
        {
            DataHandler = handler;
            Factory = new ConnectionFactory() { HostName = "localhost" };
            Connection = Factory.CreateConnection();

            InitFromUAGTMAReceiver();
            InitFromUAGOwnBoatReceiver();
        }

        private void InitFromUAGTMAReceiver()
        {

            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(exchange: "TrackData", type: ExchangeType.Fanout);
            Channel.QueueDeclare("UAGTrackDataQueue");
            Channel.QueueBind(queue: "UAGTrackDataQueue",
                              exchange: "TrackData",
                              routingKey: "");

            TMAConsumer = new EventingBasicConsumer(Channel);
            TMAConsumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                TMAOriginalMessage tmaMessage = JsonConvert.DeserializeObject<TMAOriginalMessage>(message);


                DataHandler.ReceiveTMAData(tmaMessage);
            };

            Channel.BasicConsume(queue: "UAGTrackDataQueue",
                                autoAck: true,
                                consumer: TMAConsumer);
        }

        private void InitFromUAGOwnBoatReceiver()
        {
            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(exchange: "OwnBoatData", type: ExchangeType.Fanout);
            Channel.QueueDeclare("UAGOwnBoatQueue");
            Channel.QueueBind(queue: "UAGOwnBoatQueue",
                              exchange: "OwnBoatData",
                              routingKey: "");
            OwnBoatConsumer = new EventingBasicConsumer(Channel);
            OwnBoatConsumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                OwnBoatOriginalMessage ownMessage = JsonConvert.DeserializeObject<OwnBoatOriginalMessage>(message);

                DataHandler.ReceiveOwnBoatData(ownMessage);
            };


            Channel.BasicConsume(queue: "UAGOwnBoatQueue",
                                autoAck: true,
                                consumer: OwnBoatConsumer);
        }

        private void InitFromGUIActionReceiver()
        {

            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(exchange: "Action", type: ExchangeType.Fanout);
            Channel.QueueDeclare("GuiActionQueue");
            Channel.QueueBind(queue: "GuiActionQueue",
                              exchange: "Action",
                              routingKey: "");

            ActionConsumer = new EventingBasicConsumer(Channel);
            ActionConsumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);                
            };

            Channel.BasicConsume(queue: "GuiActionQueue",
                                autoAck: true,
                                consumer: ActionConsumer);
        }

    }
}