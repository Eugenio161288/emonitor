using Euromonitor.Server.Interfaces.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euromonitor.Server.MongoDbProvider
{
    /// <summary>
    /// Represents MongoDb provider to work with MongoDb database.
    /// </summary>
    public class MongoDbProvider : IDbProvider
    {
        private MongoClient _client;

        private IMongoDatabase _db;

        /// <summary>
        /// The database name.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// The collection name.
        /// </summary>
        public string Collection { get; set; }

        /// <summary>
        /// The connection string to the database.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Adds record to the database collection asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the record.</typeparam>
        /// <param name="record">The record.</param>
        /// <returns></returns>
        public async Task InsertAsync<T>(T record)
        {
            var collection = _db.GetCollection<T>(Collection);
            await collection.InsertOneAsync(record);
        }

        /// <summary>
        /// Shows all records from db collection asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of the record.</typeparam>
        /// <returns>Returns list of the records from database collection</returns>
        public async Task<List<T>> ShowAll<T>()
        {
            var collection = _db.GetCollection<T>(Collection);

            var records = await collection.Find(new BsonDocument())
                .Project<T>(Builders<T>.Projection.Exclude("_id"))
                .ToListAsync();

            return records;
        }

        /// <summary>
        /// Initializes monglo client based on defined db name and collection string.
        /// </summary>
        public void Initialize()
        {
            if (!string.IsNullOrEmpty(ConnectionString) && !string.IsNullOrEmpty(Database))
            {
                _client = new MongoClient(ConnectionString);
                _db = _client.GetDatabase(Database);
            }
        }

        /// <summary>
        /// Updates existing document in db collection.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="record">The document value.</param>
        /// <returns></returns>
        public async Task UpdateAsync<T>(T record, string key, string value)
        {
            var collection = _db.GetCollection<T>(Collection);
            var filter = Builders<T>.Filter.Eq(key, value);

            await collection.ReplaceOneAsync(filter, record);
        }

        /// <summary>
        /// Finds docment based on passed key and value.
        /// </summary>
        /// <typeparam name="T">The type of the document.</typeparam>
        /// <param name="key">The key to be used in search filter as document field.</param>
        /// <param name="value">The value to be used in search filter as field's value.</param>
        /// <returns>Returns document based on passed key and value.</returns>
        public async Task<T> FindRecord<T>(string key, string value)
        {
            var filter = Builders<T>.Filter.Eq(key, value);
            var collection = _db.GetCollection<T>(Collection);

            var record = await collection.Find(filter)
                .Project<T>(Builders<T>.Projection.Exclude("_id"))
                .FirstOrDefaultAsync<T>();

            return record;
        }
    }
}
