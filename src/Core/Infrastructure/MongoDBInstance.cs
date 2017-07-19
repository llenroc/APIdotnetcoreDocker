using System;
using MongoDB.Driver;

namespace Domain.Core.Infrastructure
{
    public class MongoDBInstance
    {
        private static volatile MongoDBInstance instance;
        private static object syncLock = new Object();
        const string connectionString = "mongodb://localhost:27017/";
        private static IMongoDatabase db = null;

        private MongoDBInstance()
        {
            var client = new MongoClient(connectionString);
            db = client.GetDatabase("ApiDotNetCoreDB");
        }

        public static IMongoDatabase GetMongoDatabase
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                            instance = new MongoDBInstance();
                    }
                }

                return db;
            }
        }
    }
}