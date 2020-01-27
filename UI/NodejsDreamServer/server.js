// Initialize all required objects.
const app = require('express')();
const http = require('http').createServer(app);
const io = require('socket.io')(http);
const path = require('path');
const fileSystem = require('fs');

var amqp = require('amqplib/callback_api');

// Const variables
var CONSUME_LUCID_OWNBOAT_DATA = 'LucidOwnBoatData';
var CONSUME_LUCID_TRACK_DATA = 'LucidTrackData';

var PUBLISH_ACTION = 'Action';
var PUBLISH_RECORD = 'liveAudioPlayerCommands';

const CONNECTION_PATH = 'amqp://ferasg:123456@192.168.43.215';
const DEBUG_MODE = false;

// OwnBoat data object
ownBoatData = {
    messageType: "OwnBoatData",
    course: '45',
    roll: '90',
    pitch: '70',
    propeller_rpm: '125',
    diving_depth: '300'
};

// function - create random integers
const getRandomInt = (min, max) => {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
};

// print message to console log
const myLog = (message) => {
    console.log('Debug: ' + message);
};


io.on('connection', (socket) => {
    myLog('User connected');
    var ownboatRecordingId = -1;

    // Event: emitted from client
    // Purpose:
    // 1. start recordi own boat data
    // 2. start record  track data
    // 3. start record audio
    socket.on('server_startRecording', (msg) => {
        if (DEBUG_MODE) {
            myLog('message: ' + msg);
            // simulate own boat data, send it to client every 1 second
            ownboatRecordingId = setInterval(() => {
                ownBoatData.course = getRandomInt(1, 300);
                ownBoatData.roll = getRandomInt(1, 300);
                ownBoatData.pitch = getRandomInt(1, 300);
                ownBoatData.propeller_rpm = getRandomInt(1, 300);
                ownBoatData.diving_depth = getRandomInt(1, 300);

                // Emit the event in the client
                io.emit('client_ownboatData', JSON.stringify(ownBoatData));
            }, 1000);
        } else {

            // Connect to rabbitmq with the given connection path 
            // And publish a Record message to the 'Action' exchange 
            // In order to start the data  
            amqp.connect(CONNECTION_PATH, (error0, connection) => {
                if (error0) {
                    throw error0;
                }
                connection.createChannel((error1, channel) => {
                    if (error1) {
                        throw error1;
                    }

                    var exchange = PUBLISH_ACTION;

                    channel.assertExchange(exchange, 'fanout', {
                        durable: false
                    });
                    channel.publish(exchange, '', Buffer.from('Record'));
                    console.log(" [x] Sent %s", msg);


                    // Consume ownboat data from 'LucidOwnBoatData' exchange 
                    // And handle the receving data
                    var exchange = CONSUME_LUCID_OWNBOAT_DATA;

                    channel.assertExchange(exchange, 'fanout', {
                        durable: false
                    });

                    channel.assertQueue('', {
                        exclusive: true
                    }, (error2, q) => {
                        if (error2) {
                            throw error2;
                        }
                        console.log(" [*] Waiting for ownboat data messages in %s. To exit press CTRL+C", q.queue);
                        channel.bindQueue(q.queue, exchange, '');

                        channel.consume(q.queue, (msg) => {
                            if (msg.content) {
                                ownboatDataHandler(decodeURIComponent(msg.content));
                                console.log(" [x] %s", decodeURIComponent(msg.content));
                            }
                        }, {
                            noAck: true
                        });
                    });
                });


                // Consume track data data from 'LucidTrackData' exchange 
                // And handle the receving data
                connection.createChannel((error1, channel) => {
                    if (error1) {
                        throw error1;
                    }

                    var exchange = CONSUME_LUCID_TRACK_DATA;

                    channel.assertExchange(exchange, 'fanout', {
                        durable: false
                    });

                    channel.assertQueue('', {
                        exclusive: true
                    }, (error2, q) => {
                        if (error2) {
                            throw error2;
                        }
                        console.log(" [*] Waiting for track data messages in %s. To exit press CTRL+C", q.queue);
                        channel.bindQueue(q.queue, exchange, '');

                        channel.consume(q.queue, (msg) => {
                            if (msg.content) {
                                trackDataHandler(decodeURIComponent(msg.content));
                            }
                        }, {
                            noAck: true
                        });
                    });
                });
            });
        }


    });

    // Event: emitted from client
    // Purpose:
    // 1. stop recordi own boat data
    // 2. stop record  track data
    // 3. stop record audio
    socket.on('server_stopRecording', (msg) => {
        if (DEBUG_MODE) {
            myLog('message: ' + msg + " ownboat interval id: " + ownboatRecordingId);
            clearInterval(ownboatRecordingId);
        } else {


            // Send Stop message to the 'Action' exchange 
            // In order to stop the data 
            amqp.connect(CONNECTION_PATH, (error0, connection) => {
                if (error0) {
                    throw error0;
                }
                connection.createChannel((error1, channel) => {
                    if (error1) {
                        throw error1;
                    }
                    var exchange = PUBLISH_ACTION;

                    channel.assertExchange(exchange, 'fanout', {
                        durable: false
                    });
                    channel.publish(exchange, '', Buffer.from('Stop'));
                    console.log(" [x] Sent %s", msg);
                });
            });
        }
    });

    // Emitted automatically when user disconnected
    socket.on('server_play', (msg) => {

        // TODO:
    });

    socket.on('server_chosenTrackId', (msg) => {

        // Send the given track id to the 'liveAudioPlayerCommands' exchange
        amqp.connect(CONNECTION_PATH, (error0, connection) => {
            if (error0) {
                throw error0;
            }
            connection.createChannel((error1, channel) => {
                if (error1) {
                    throw error1;
                }

                var exchange = PUBLISH_RECORD;

                channel.assertExchange(exchange, 'fanout', {
                    durable: true
                });
                channel.publish(exchange, '', Buffer.from(msg));
                console.log(" [x] Sent %s", msg);
            });
        });
    });

    socket.on('disconnect', () => {
        myLog('user disconnected');
    });
});

// The server start listen at port 3000
http.listen(3000, () => {
    myLog('listening on *:3000');
});


app.get('/wav', (req, res, err) => {
    // generate file path
    myLog(__dirname);
    const filePath = path.resolve(__dirname, './wav', 'track.wav');

    // get file size info
    const stat = fileSystem.statSync(filePath);

    // set response header info
    res.writeHead(200, {
        'Content-Type': 'audio/mpeg',
        'Content-Length': stat.size
    });
    //create read stream
    const readStream = fileSystem.createReadStream(filePath);
    // attach this stream with response stream
    readStream.pipe(res);
});

const ownboatDataHandler = (data) => {
    console.log("ownboat Data" + data);
    io.emit('client_ownboatData', data);
};

const trackDataHandler = (data) => {
    console.log('track Data' + data);
    io.emit('client_trackData', data);
};