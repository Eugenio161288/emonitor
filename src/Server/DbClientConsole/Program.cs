using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace DbClientConsole
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //MongoClient client = new MongoClient("mongodb://sysuser:05102020password@ds040167.mlab.com:40167/emonitor_db");
            //var db = client.GetDatabase("emonitor_db");

            //var collection = db.GetCollection<Book>("books");

            //var books = collection.Find(new BsonDocument())
            //    .Project<Book>(Builders<Book>.Projection.Exclude("_id"))
            //    .ToList();

            List<string> books = new List<string> { "book1", "book2", "book3" };
            books.Remove("book1");



            var dbBuilder = new MongoDbProviderBuilder();

            var dbProvider = dbBuilder.SetConnectionString("mongodb://sysuser:05102020password@ds040167.mlab.com:40167/emonitor_db")
                                .SetDatabase("emonitor_db")
                                .SetCollection("books")
                                .Build();

            var result = await dbProvider.ShowAll<Book>();

            Console.WriteLine("Async db data retrieving...");

            //return books;

            //var filter = Builders<BsonDocument>.Filter.Eq("name", "Soft Skills");

            //var book = collection.Find(filter).FirstOrDefault();
        }
    }
}
