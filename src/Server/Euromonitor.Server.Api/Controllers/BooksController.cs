using Euromonitor.Server.Api.Models.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euromonitor.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Book>> Show()
        {
            MongoClient client = new MongoClient("mongodb://sysuser:05102020password@ds040167.mlab.com:40167/emonitor_db?retryWrites=false");
            var db = client.GetDatabase("emonitor_db");

            var collection = db.GetCollection<Book>("books");

            var books = await collection.Find(new BsonDocument())
                .Project<Book>(Builders<Book>.Projection.Exclude("_id"))                                                                                                                                                                                        
                .ToListAsync();

            return books;

            //var filter = Builders<BsonDocument>.Filter.Eq("name", "Soft Skills");

            //var book = collection.Find(filter).FirstOrDefault();
        }
    }
}
