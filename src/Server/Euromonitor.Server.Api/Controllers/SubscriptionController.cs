using Euromonitor.Server.Api.Models;
using Euromonitor.Server.Api.Models.Data;
using Euromonitor.Server.Interfaces.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Euromonitor.Server.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private IDbProviderBuilder _dbBuilder;

        private IConfiguration _configuration;

        public SubscriptionController(IDbProviderBuilder dbProviderBuilder, IConfiguration configuration)
        {
            _dbBuilder = dbProviderBuilder;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<List<Book>> Books()
        {
            List<Book> result = null;
            var connectionString = _configuration["connectionString"];

            var dbProvider = _dbBuilder.SetConnectionString(connectionString)
                                .SetDatabase("emonitor_db")
                                .SetCollection("users")
                                .Build();

            var givenName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            var user = await dbProvider.FindRecord<User>("givenname", givenName.Value);
            if (user != null && user.Books.Any())
            {
                result = user.Books;
            }


            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookRequestModel model)
        {
            var connectionString = _configuration["connectionString"];

            var dbProvider = _dbBuilder.SetConnectionString(connectionString)
                                .SetDatabase("emonitor_db")
                                .SetCollection("books")
                                .Build();
            var book = await dbProvider.FindRecord<Book>("isbn", model.Isbn);

            dbProvider.Collection = "users";
            // move to the helper
            var givenName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            var user = await dbProvider.FindRecord<User>("givenname", givenName.Value);

            //Console.WriteLine(User.Claims);
            if (user == null)
            {
                // move to the fabric
                var newUser = new User { GivenName = givenName.Value, Books = new List<Book> { book } };
                await dbProvider.InsertAsync<User>(newUser);
            }
            else
            {
                // check if books is already subscribed
                var existingBook = user.Books.Contains(book);
                if(!existingBook)
                {
                    user.Books.Add(book);
                    await dbProvider.UpdateAsync<User>(user, "givenname", givenName.Value);
                }
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook([FromBody] BookRequestModel model)
        {
            var connectionString = _configuration["connectionString"];

            var dbProvider = _dbBuilder.SetConnectionString(connectionString)
                                .SetDatabase("emonitor_db")
                                .SetCollection("books")
                                .Build();
            var book = await dbProvider.FindRecord<Book>("isbn", model.Isbn);

            dbProvider.Collection = "users";
            // move to the helper
            var givenName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            //var claimJson = new JsonResult(User.Claims.Select(c => new { c.Type, c.Value }));
            var user = await dbProvider.FindRecord<User>("givenname", givenName.Value);

            if(user != null && book != null)
            {
                // check if books is already subscribed
                var existingBook = user.Books.Contains(book);
                if (existingBook)
                {
                    user.Books.Remove(book);
                    await dbProvider.UpdateAsync<User>(user, "givenname", givenName.Value);
                }

                //user.Books.Remove(book);
                //await dbProvider.UpdateAsync<User>(user, "givenname", givenName.Value);
            }

            return Ok();
        }
    }
}
