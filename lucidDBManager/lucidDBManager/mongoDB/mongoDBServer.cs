using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.IO;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json;
using System.Net;
using lucidDBManager.Data;
using MongoDB.Bson.IO;
using Newtonsoft.Json.Linq;
using static lucidDBManager.Data.BasicData;

namespace lucidDBManager.mongoDB
{
    public class MongoDBServer
    {
        private IMongoDatabase _db;

        #region Main Functions
        public void initDB(string dbName)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");

            //Database List  
            var dbList = dbClient.ListDatabases().ToList();

            Console.WriteLine("The list of databases are :");
            foreach (var item in dbList)
            {
                Console.WriteLine(item);
            }
            //Get Database and Collection  
            _db = dbClient.GetDatabase(dbName);
            var collList = _db.ListCollections().ToList();
            Console.WriteLine("The list of collections are :");
            foreach (var item in collList)
            {
                Console.WriteLine(item);
            }
        }

        public void saveRecord(object message, string collectionName)
        {
            IMongoCollection<BsonDocument> collection = _db.GetCollection<BsonDocument>(collectionName);
            string jsonFile = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            collection.InsertOne(BsonDocument.Parse(jsonFile));
        }

        public void deleteMessage(string collectionName, BsonDocument element)
        {
            IMongoCollection<BsonDocument> collection = _db.GetCollection<BsonDocument>(collectionName);

            collection.FindOneAndDelete(element);
        }
              

        public List<SystemTracks> getTracksByID(long trackID) 
        {
            IMongoCollection<BsonDocument> collection = _db.GetCollection<BsonDocument>("SystemTrack");
            var filter = Builders<BsonDocument>.Filter.Eq("systemTracks.trackID", trackID);
            var result = collection.Find(filter).ToList();
            var listOfTMAMessage = new List<SystemTracks>();
            foreach(var mes in result)
            {
                var x = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JObject json = JObject.Parse(mes.ToJson<MongoDB.Bson.BsonDocument>(x));

                SystemTracks tmaMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<SystemTracks>(json.ToString());
                
                SystemTracks UpdatedMessge = new SystemTracks();
                UpdatedMessge.timeStamp = tmaMessage.timeStamp;

                for (int i = 0; i < tmaMessage.systemTracks.Count; i++)
                {

                    if(tmaMessage.systemTracks[i].trackID == trackID)
                    {
                        TrackData neededTrack = new TrackData()
                        {
                            creationTime = tmaMessage.systemTracks[i].creationTime,
                            relativeBearing = tmaMessage.systemTracks[i].relativeBearing,
                            relativeBearingRate = tmaMessage.systemTracks[i].relativeBearingRate,
                            trackID = tmaMessage.systemTracks[i].trackID,
                            trackState = tmaMessage.systemTracks[i].trackState
                        };

                        UpdatedMessge.systemTracks = new List<TrackData>();
                        UpdatedMessge.systemTracks.Add(neededTrack);
                    }
                }
                listOfTMAMessage.Add(UpdatedMessge);
            }

            return listOfTMAMessage;
        }

        public List<SystemTracks> getFullTrackMessagesByTime(TimeType startTime, TimeType endTime)
        {
            List<SystemTracks> TrackMessages = new List<SystemTracks>();
            IMongoCollection<BsonDocument> collection = _db.GetCollection<BsonDocument>("SystemTrack");
            var filter1 = Builders<BsonDocument>.Filter.Gt("timeStamp.seconds", startTime.seconds);
            var filter2 = Builders<BsonDocument>.Filter.Lt("timeStamp.seconds", endTime.seconds);
            var mainFilter = Builders<BsonDocument>.Filter.And(filter1, filter2);
            var result = collection.Find(mainFilter).ToList();

            foreach (var trackRecord in result)
            {
                var tmp = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JObject json = JObject.Parse(trackRecord.ToJson<MongoDB.Bson.BsonDocument>(tmp));

                SystemTracks trackMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<SystemTracks>(json.ToString());

                TrackMessages.Add(trackMessage);
            }            

            return TrackMessages;
        }

        public List<OwnBoatData> getOwnBoatByTime(TimeType startTime, TimeType endTime)
        {
            List<OwnBoatData> OwnBoatMessages = new List<OwnBoatData>();
            IMongoCollection<BsonDocument> collection = _db.GetCollection<BsonDocument>("OwnBoat");
            var filter1 = Builders<BsonDocument>.Filter.Gt("timeStamp.seconds", startTime.seconds);
            var filter2 = Builders<BsonDocument>.Filter.Lt("timeStamp.seconds", endTime.seconds);
            var mainFilter = Builders<BsonDocument>.Filter.And(filter1, filter2);
            var result = collection.Find(mainFilter).ToList();

            foreach (var ownBoatRecord in result)
            {
                var tmp = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JObject json = JObject.Parse(ownBoatRecord.ToJson<MongoDB.Bson.BsonDocument>(tmp));

                OwnBoatData ownboatMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<OwnBoatData>(json.ToString());

                OwnBoatMessages.Add(ownboatMessage);
            }

            return OwnBoatMessages;
        }

        public List<OwnBoatData> getAllOwnBoatData()
        {
            List<OwnBoatData> result = new List<OwnBoatData>();

            var allMessages = getMessages("OwnBoat");

            foreach (var message in allMessages)
            {
                var x = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JObject json = JObject.Parse(message.ToJson<MongoDB.Bson.BsonDocument>(x));

                OwnBoatData ownBoatRecord = Newtonsoft.Json.JsonConvert.DeserializeObject<OwnBoatData>(json.ToString());

                result.Add(ownBoatRecord);
            }          
            
            return result;
        }

        #endregion
        #region Private Functions

        private string ToBson<T>(T value)
        {
            using (MemoryStream ms = new MemoryStream())
            using (BsonDataWriter datawriter = new BsonDataWriter(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(datawriter, value);
                return Convert.ToBase64String(ms.ToArray());
            }

        }

        private T FromBson<T>(string base64data)
        {
            byte[] data = Convert.FromBase64String(base64data);

            using (MemoryStream ms = new MemoryStream(data))
            using (BsonDataReader reader = new BsonDataReader(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<T>(reader);
            }
        }

        private List<BsonDocument> getMessages(string collectionName)
        {
            //READ  
            IMongoCollection<BsonDocument> collection = _db.GetCollection<BsonDocument>(collectionName);
            var documentsList = collection.Find(new BsonDocument()).ToList();
            foreach (var item in documentsList)
            {
                Console.WriteLine(item.ToString());
            }

            return documentsList;
        }
        #endregion
    }
}
