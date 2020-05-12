using Euromonitor.Server.MongoDbProvider;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbClientConsole
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //MongoClient client = new MongoClient("mongodb://sysuser:05102020password@ds040167.mlab.com:40167/emonitor_db?retryWrites=false");
            //var db = client.GetDatabase("emonitor_db");

            //var collection = db.GetCollection<Book>("books");

            //var books = collection.Find(new BsonDocument())
            //    .Project<Book>(Builders<Book>.Projection.Exclude("_id"))
            //    .ToList();

            //var userCollection = db.GetCollection<User>("users");

            //var filter = Builders<User>.Filter.Eq("firstName", "test");

            //// find user
            //var user = userCollection.Find(filter)
            //    .Project<User>(Builders<User>.Projection.Exclude("_id"))
            //    .FirstOrDefault<User>();

            //if (user != null)
            //{
            //    user.LastName = "Test Name changed";
            //    var userFilter = Builders<User>.Filter.Eq(s => s.FirstName, "test");
            //    user.Books = books;
            //    await userCollection.ReplaceOneAsync(userFilter, user);
            //}

            await AddBook("test1", "Cloud Native");

            //List<string> books = new List<string> { "book1", "book2", "book3" };
            //books.Remove("book1");



            //var dbBuilder = new MongoDbProviderBuilder();

            //var dbProvider = dbBuilder.SetConnectionString("mongodb://sysuser:05102020password@ds040167.mlab.com:40167/emonitor_db")
            //                    .SetDatabase("emonitor_db")
            //                    .SetCollection("books")
            //                    .Build();

            //var books = await dbProvider.ShowAll<Book>();

            Console.WriteLine("Async db data retrieving...");


            //dbProvider = dbBuilder.SetConnectionString("mongodb://sysuser:05102020password@ds040167.mlab.com:40167/emonitor_db")
            //                    .SetDatabase("emonitor_db")
            //                    .SetCollection("users")
            //                    .Build();

            //var users = await dbProvider.ShowAll<User>();

            Console.WriteLine("Async db data retrieving...");
        }

        static private async Task AddBook(string userName, string bookName)
        {
            // 1. user identity and book name
            // 2. find user
            // 3. find book
            // 4. prepare in-memory object
            // 5. update user in db

            var dbBuilder = new MongoDbProviderBuilder();

            var dbProvider = dbBuilder.SetConnectionString("mongodb://sysuser:05102020password@ds040167.mlab.com:40167/emonitor_db?retryWrites=false")
                                        .SetDatabase("emonitor_db")
                                        .SetCollection("books")
                                        .Build();


            var book = await dbProvider.FindRecord<Book>("name", bookName);

            dbProvider.Collection = "users";
            var user = await dbProvider.FindRecord<User>("givenname", userName);
            if(user == null)
            {
                // move to the fabric
                var newUser = new User { GivenName = userName, Books = new List<Book> { book } };
                await dbProvider.InsertAsync<User>(newUser);
            }
            else
            {
                // check if books is already subscribed
                var existingBook = user.Books.Select(b => b.Name == book.Name).FirstOrDefault();

                user.Books.Add(book);
                await dbProvider.UpdateAsync<User>(user, "givenname", userName);
            }

            Console.WriteLine("book in the subscription");
        }
    }
}
