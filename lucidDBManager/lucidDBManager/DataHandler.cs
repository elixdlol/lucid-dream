using System;
using System.Collections.Generic;
using System.Text;
using lucidDBManager.Data;
using lucidDBManager.mongoDB;
using lucidDBManager.RabbitMQ;
using Newtonsoft.Json;

namespace lucidDBManager
{
    public class DataHandler
    {
        RabbitMQSender sender;

        MongoDBServer db;

        bool[] isKnownTarget;
        TimeStampType[] creationTime;

        public DataHandler(RabbitMQSender sender, MongoDBServer db)
        {
            this.sender = sender;
            this.db = db;
            isKnownTarget = new bool[26];
            creationTime = new TimeStampType[26];

            for (int i = 0; i < isKnownTarget.Length; i++)
            {
                isKnownTarget[i] = false;
            }
        }

        // Recieves a string in a Json format.
        // Handle the Received TMA message
        public void ReceiveTMAData(TMAOriginalMessage receivedMessage)
        {
            HandleTMAMessage(receivedMessage);
        }

        // Handle a TMA message
        public void HandleTMAMessage(TMAOriginalMessage message)
        {
            SystemTracks sysTracks = new SystemTracks();
            sysTracks.timeStamp = message.timeStamp;
            sysTracks.systemTracks = new List<TrackData>();

            foreach (OriginalSystemTrack OrigTrack in message.systemTracks)
            {
                // if track exists
                if (OrigTrack.trackId != 0)
                {                 
                    TrackData newTrackData = new TrackData();
 
                    newTrackData.trackID = OrigTrack.trackId;

                    newTrackData.relativeBearing = OrigTrack.bearing;

                    if (OrigTrack.bearingRate.valid)
                    {
                        newTrackData.relativeBearingRate = OrigTrack.bearingRate.value;
                    }

                    // if new track
                    if (!isKnownTarget[OrigTrack.trackId - 1])
                    {
                        isKnownTarget[OrigTrack.trackId - 1] = true;
                        creationTime[OrigTrack.trackId - 1] = OrigTrack.timeStamp;
                        
                        newTrackData.creationTime = OrigTrack.timeStamp;
                    }
                    // if old track
                    else
                    {
                        newTrackData.creationTime = creationTime[OrigTrack.trackId - 1];
                    }

                    sysTracks.systemTracks.Add(newTrackData);
                }

                // send to stiching
                sender.sendTrackData(sysTracks);

                // save to db
                db.saveRecord(sysTracks, "SystemTrack");
            }
        }

        public void ReceiveOwnBoatData()
        {

        }
    }
}
