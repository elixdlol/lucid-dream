using System;
using System.Collections.Generic;
using System.Text;
using lucidDBManager.Data;
using lucidDBManager.mongoDB;
using lucidDBManager.RabbitMQ;
using Newtonsoft.Json;
using static lucidDBManager.Data.BasicData;
using static lucidDBManager.Data.BasicOriginalData;

namespace lucidDBManager
{
    public class DataHandler
    {
        RabbitMQSender sender;

        MongoDBServer db;

        // system track helper types
        bool[] isKnownTarget;
        TimeStampType[] creationTime;
        TMAOriginalMessage lastTracksMessage;


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

            sysTracks.timeStamp = convertTime(message.timeStamp);

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

                        newTrackData.trackState = State.NewTrack;
                        newTrackData.creationTime = convertTime(OrigTrack.timeStamp);
                    }
                    // if old track
                    else
                    {
                        newTrackData.trackState = State.UpdateTrack;
                        newTrackData.creationTime = convertTime(creationTime[OrigTrack.trackId - 1]);
                    }

                    sysTracks.systemTracks.Add(newTrackData);
                }
            }

            if (lastTracksMessage != null)
            {

                foreach (var currTrack in lastTracksMessage.systemTracks)
                {
                    // check if track was deleted
                    if (!sysTracks.systemTracks.Exists(x => x.trackID == currTrack.trackId))
                    {
                        isKnownTarget[currTrack.trackId - 1] = false;
                        TrackData newTrack = new TrackData()
                        {
                            trackID = currTrack.trackId,
                            trackState = State.DeleteTrack
                        };

                        sysTracks.systemTracks.Add(newTrack);
                    }
                }
            }

            lastTracksMessage = message;

            // send to stiching
            sender.SendTrackData(sysTracks);

            // save to db
            db.saveRecord(sysTracks, "SystemTrack");
        }

        // Recieves a string in a Json format.
        // Handle the Received OwnBoat message
        public void ReceiveOwnBoatData(OwnBoatOriginalMessage receivedMessage)
        {
            HandleOwnBoatMessage(receivedMessage);
        }

        // Handle the Own Boat message
        public void HandleOwnBoatMessage(OwnBoatOriginalMessage message)
        {
            OwnBoatData ownBoat = new OwnBoatData();

            // convert
            ownBoat.timeStamp = convertTime(message.systemTime.time.value);
            ownBoat.timeZone = message.timeZone.data.value;
            ownBoat.heading = message.heading.data.value;
            ownBoat.pitch = message.pitch.data.value;
            ownBoat.roll = message.roll.data.value;
            ownBoat.heave = message.heave.data.value;


            sender.SendOwnBoatData(ownBoat);

            //save to db
            db.saveRecord(ownBoat, "OwnBoat");
        }

        public TimeType convertTime(TimeStampType origType)
        {
            TimeType newType;
            newType.c_seconds = origType.time.c_seconds;
            newType.seconds = origType.time.seconds;
            newType.minutes = origType.time.minutes;
            newType.hours = origType.time.hours;
            newType.day = origType.date.day;
            newType.month = origType.date.month;
            newType.year = origType.date.year;

            return newType;
        }

        public void GetOfflineTrackData()
        {
            // get track data by id from db
        }

        public void GetOfflineAudioFile()
        {
            // get wav file by id
        }

        public void GetOfflineOwnBoatData()
        {
            // get own boat data by id from db
        }
    }
}
