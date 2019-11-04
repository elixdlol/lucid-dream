using System;
using System.Collections.Generic;
using System.Text;
using lucidDBManager.Data;
using lucidDBManager.RabbitMQ;
using Newtonsoft.Json;

namespace lucidDBManager
{
    public class DataHandler
    {
        RabbitMQSender sender;

        DataHandler(RabbitMQSender sender)
        {
            this.sender = sender;
        }

        // Recieves a string in a Json format.
        // Handle the Received TMA message, then send it to the sticher
        // and save it to the DB.
        public void ReceiveTMAData(string receivedMessage)
        {
            TMAOriginalMessage message = (JsonConvert.DeserializeObject(receivedMessage)) as TMAOriginalMessage;

            SystemTracks systemTracks = new SystemTracks();

            foreach (OriginalSystemTrack OrigTrack in message.systemTracks)
            {
                TrackData track = new TrackData()
                {
                    trackID = OrigTrack.trackId,
                    relativeBearing = OrigTrack.bearing
                };
            }

            sender.sendTrackData(systemTracks);
        }

        public void ReceiveOwnBoatData()
        {

        }

        //public void SendDataToStiching(TrackData trackdata)
        //{
        //    // Send Data
        //}
    }
}
