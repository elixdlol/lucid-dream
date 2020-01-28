var amqp = require('amqplib/callback_api');

module.exports = {

    publish: function (exchangeName, routingKey, message, exchangeType) {

        //'amqp://localhost'
        amqp.connect('amqp://rutush:123456@172.16.20.161', function (error0, connection) {
            if (error0) {
                throw error0;
            }
            connection.createChannel(function (error1, channel) {
                if (error1) {
                    throw error1;
                }

                // var queueName = 'audioMessages';
                var msg = 'audio msg';

                channel.assertExchange(exchangeName, exchangeType, {
                    durable: false
                });

                channel.publish(exchangeName, routingKey, Buffer.from(message));

                //  channel.bindQueue(queueName, exchangeName, routingKey);

                console.log(" [x] Sent %s", message);

            });

            setTimeout(function () {
                connection.close();
                //process.exit(0);
            }, 500);

        });
    },

    consume: async function (handler, exchangeName, routingKey, queueName, exchangeType) {
        return new Promise(resolve => {
            //'amqp://rutush:123456@172.16.20.161'
            amqp.connect('amqp://rutush:123456@172.16.20.161', function (error0, connection) {
                if (error0) {
                    console.log('error in connection');
                    throw error0;
                }
                connection.createChannel(function (error1, channel) {
                    if (error1) {
                        throw error1;
                    }

                    channel.assertExchange(exchangeName, exchangeType, {
                        durable: false
                    });

                    channel.assertQueue(queueName, function (error2, q) {
                        //   if (error2) {
                        //       throw error2;
                        //   }
                        console.log(" [*] Waiting for messages in %s. To exit press CTRL+C", queueName);
                        channel.bindQueue(queueName, exchangeName, routingKey);
                        channel.consume(queueName, function (msg) {
                            if (msg.content) {
                                console.log(" [x] %s", msg.content.toString());
                                message = msg.content.toString();
                                handler(message);
                            }
                            console.log('.....');
                            setTimeout(function () {
                                console.log("Message:", msg.content.toString());
                            }, 4000);
                        }, {
                            noAck: true
                        });

                        resolve(channel);
                        //return channel.consume(q.queue, function (msg) {
                        //    if (msg.content) {
                        //        console.log(" [x] %s", msg.content.toString());
                        //        message = msg.content.toString;
                        //    }
                        //    console.log('.....');
                        //    setTimeout(function () {
                        //        console.log("Message:", msg.content.toString());
                        //    }, 4000);
                        //},
                        //{
                        //    noAck: true
                        //});
                    });
                });
            });
        });
    }
}