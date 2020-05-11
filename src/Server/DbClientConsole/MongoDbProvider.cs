using Euromonitor.Server.Interfaces.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbClientConsole
{
    public class MongoDbProvider : IDbProvider
    {
        private MongoClient _client;

        private IMongoDatabase _db;

        public string Database { get; set; }

        public string Collection { get; set; }

        public string ConnectionString { get; set; }

        public Task InsertAsync<T>(T record)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> ShowAll<T>()
        {
            var collection = _db.GetCollection<T>(Collection);

            var books = await collection.Find(new BsonDocument())
                .Project<T>(Builders<T>.Projection.Exclude("_id"))
                .ToListAsync();

            return books;
        }

        public void Build()
        {
            if(!string.IsNullOrEmpty(ConnectionString) && !string.IsNullOrEmpty(Database))
            {
                _client = new MongoClient(ConnectionString);
                _db = _client.GetDatabase(Database);
            }
        }
    }
}
