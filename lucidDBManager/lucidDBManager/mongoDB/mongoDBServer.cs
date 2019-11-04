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

namespace lucidDBManager.mongoDB
{
    class mongoDBServer
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

        public void createMessage(string collectionName, string jsonFile) 
        {
            IMongoCollection<BsonDocument> collection = _db.GetCollection<BsonDocument>(collectionName);

            var document = BsonDocument.Parse(jsonFile);
            collection.InsertOne(document);
        }

        public void deleteMessage(string collectionName, BsonDocument element)
        {
            IMongoCollection<BsonDocument> collection = _db.GetCollection<BsonDocument>(collectionName);

            collection.FindOneAndDelete(element);
        }
        

        public List<BsonDocument> getMessages(string collectionName)
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
        #endregion
    }
}
