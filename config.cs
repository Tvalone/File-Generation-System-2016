using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace File_Generation_System
{
    public class config
    {
        public ObjectId id { get; set; }
        public string requestor_code { get; set; }
        public string FirstName { get; set; }
        //public MongoDatabase db;

        //public  MongoDatabase GetConnectedToMongo()
        //{
        //    var connectionString = "mongodb://localhost";
        //    MongoClient client = new MongoClient(connectionString);
        //    MongoServer server = client.GetServer();
        //    db = server.GetDatabase("test");
           
             

        //    return db;
        //}
        //public string returnNames() 
        //{
        //    var names = db.GetCollection<BsonDocument>("FirstName").ToString();
        //    return names;
        //}
    } 

  

 }
