using Euromonitor.Server.Api.Models;
using Euromonitor.Server.Api.Models.Configuration;
using Euromonitor.Server.Interfaces.Database;
using Euromonitor.Server.MongoDbProvider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Euromonitor.Server.Api.Controllers
{
    /// <summary>
    /// Determines REST API for user subscriptions.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private IDbProviderBuilder _dbBuilder;

        private DbConfiguration _dbConfiguration;

        public SubscriptionController(IDbProviderBuilder dbProviderBuilder, IConfiguration configuration)
        {
            _dbBuilder = dbProviderBuilder;
            _dbConfiguration = configuration.GetSection("databaseSettings").Get<DbConfiguration>();
        }

        /// <summary>
        /// Shows all books from user subscription.
        /// </summary>
        /// <returns>Returns list of the books from user subscription.</returns>
        [HttpGet]
        public async Task<List<Book>> UserBooks()
        {
            List<Book> result = null;

            var dbProvider = _dbBuilder.SetConnectionString(_dbConfiguration.ConnectionString)
                                .SetDatabase(_dbConfiguration.Database)
                                .SetCollection(_dbConfiguration.UsersCollection)
                                .Build();

            var givenName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            var user = await dbProvider.FindRecord<User>("givenname", givenName.Value);
            if (user != null && user.Books.Any())
            {
                result = user.Books;
            }


            return result;
        }

        /// <summary>
        /// Adds new book to the user subscription.
        /// </summary>
        /// <param name="model">Book's model to be added to the user subscription.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookRequestModel model)
        {
            var dbProvider = _dbBuilder.SetConnectionString(_dbConfiguration.ConnectionString)
                                .SetDatabase(_dbConfiguration.Database)
                                .SetCollection(_dbConfiguration.BooksCollection)
                                .Build();
            var book = await dbProvider.FindRecord<Book>("isbn", model.Isbn);

            dbProvider.Collection = _dbConfiguration.UsersCollection;
            var givenName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            var user = await dbProvider.FindRecord<User>("givenname", givenName.Value);

            if (user == null)
            {
                // move to the fabric
                var newUser = new User { GivenName = givenName.Value, Books = new List<Book> { book } };
                await dbProvider.InsertAsync<User>(newUser);
            }
            else
            {
                var existingBook = user.Books.Contains(book);
                if(!existingBook)
                {
                    user.Books.Add(book);
                    await dbProvider.UpdateAsync<User>(user, "givenname", givenName.Value);
                }
            }

            return Ok();
        }

        /// <summary>
        /// Removes book from the user subscription.
        /// </summary>
        /// <param name="model">The book's model to be removed from user subscription.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteBook([FromBody] BookRequestModel model)
        {
            var dbProvider = _dbBuilder.SetConnectionString(_dbConfiguration.ConnectionString)
                                .SetDatabase(_dbConfiguration.Database)
                                .SetCollection(_dbConfiguration.BooksCollection)
                                .Build();
            var book = await dbProvider.FindRecord<Book>("isbn", model.Isbn);

            dbProvider.Collection = _dbConfiguration.UsersCollection;
            var givenName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            var user = await dbProvider.FindRecord<User>("givenname", givenName.Value);

            if(user != null && book != null)
            {
                var existingBook = user.Books.Contains(book);
                if (existingBook)
                {
                    user.Books.Remove(book);
                    await dbProvider.UpdateAsync<User>(user, "givenname", givenName.Value);
                }
            }

            return Ok();
        }
    }
}
