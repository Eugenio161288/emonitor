using Euromonitor.Server.MongoDbProvider.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Euromonitor.Server.MongoDbProvider.IntegrationTests
{
    public class MongoDbProviderTests : IDisposable
    {
        private const string ConnectionString = "mongodb://emonitoradmin:Password_05132020@ds040167.mlab.com:40167/euro_monitor_db_dev?retryWrites=false";

        private MongoDbProvider _dbProvider;

        private MongoClient _client;

        public MongoDbProviderTests()
        {
            _dbProvider = new MongoDbProvider();
            _dbProvider.ConnectionString = ConnectionString;
            _dbProvider.Database = "euro_monitor_db_dev";
            _dbProvider.Collection = "users_test";
            _dbProvider.Initialize();
            _client = new MongoClient(ConnectionString);
        }

        [Fact]
        public async Task InsertAsync_NotEmptyRecord_AddsProvidedRecordToDb()
        {
            var user = new User { GivenName = "testuser", FirstName = "John", LastName = "Doe", Email = "email@gmail.com" };

            var db = _client.GetDatabase("euro_monitor_db_dev");
            var filter = Builders<BsonDocument>.Filter.Eq("givenname", "testuser");
            var collection = db.GetCollection<BsonDocument>("users_test");
            // no user in db
            var actualUser = await collection.Find(filter).FirstOrDefaultAsync();
            Assert.Null(actualUser);

            _dbProvider.Collection = "users_test";
            await _dbProvider.InsertAsync<User>(user);

            actualUser = await collection.Find(filter).FirstOrDefaultAsync();

            Assert.NotNull(actualUser);

            collection.DeleteOne(filter);
        }

        [Fact]
        public async Task UpdateAsync_NotEmptyRecord_DoesNotUpdateNotExitingRecord()
        {
            var book = new Book { Name = "Cloud Native", Isbn = "3445231243", Text = "Long Text", CoverUrl = "URL" };
            var db = _client.GetDatabase("euro_monitor_db_dev");
            var filter = Builders<BsonDocument>.Filter.Eq("isbn", "3445231243");
            var collection = db.GetCollection<BsonDocument>("books_test");
            var actualBook = await collection.Find(filter).FirstOrDefaultAsync();

            Assert.Null(actualBook);

            _dbProvider.Collection = "books_test";
            await _dbProvider.UpdateAsync<Book>(book, "isbn", "3445231243");

            actualBook = await collection.Find(filter).FirstOrDefaultAsync();

            Assert.Null(actualBook);
        }

        [Fact]
        public async Task UpdateAsync_NotEmptyRecord_UpdatesExistingRecord()
        {
            
            var dbBook = new BsonDocument
            {
                { "isbn", "984545" },
                { "name", "Patterns" },
                { "text", "Long Text" },
                { "coverUrl", "URL" }
            };
            var db = _client.GetDatabase("euro_monitor_db_dev");
            var filter = Builders<BsonDocument>.Filter.Eq("isbn", "984545");
            var collection = db.GetCollection<BsonDocument>("books_test");
            var actualBook = await collection.Find(filter).FirstOrDefaultAsync();

            Assert.Null(actualBook);

            await collection.InsertOneAsync(dbBook);
            actualBook = await collection.Find(filter).FirstOrDefaultAsync();

            Assert.NotNull(actualBook);
            Assert.Equal("Patterns", actualBook["name"]);

            var book = new Book { Name = "Changed name", Isbn = "984545", Text = "Long Text", CoverUrl = "URL" };

            _dbProvider.Collection = "books_test";
            await _dbProvider.UpdateAsync<Book>(book, "isbn", "984545");

            actualBook = await collection.Find(filter).FirstOrDefaultAsync();

            Assert.NotNull(actualBook);
            Assert.Equal(book.Name, actualBook["name"]);

            collection.DeleteOne(filter);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //users.Clear();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MongoDbProviderTests()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
