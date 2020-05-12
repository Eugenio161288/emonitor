using Euromonitor.Server.Api.Models.Data;
using Euromonitor.Server.Interfaces.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euromonitor.Server.Api.Controllers
{
    /// <summary>
    /// Determines REST API methods for books.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IDbProviderBuilder _dbBuilder;

        private IConfiguration _configuration;

        public BooksController(IDbProviderBuilder dbProviderBuilder, IConfiguration configuration)
        {
            _dbBuilder = dbProviderBuilder;
            _configuration = configuration;
        }

        /// <summary>
        /// Shows all books.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Book>> ShowAll()
        {
            var connectionString = _configuration["connectionString"];

            var dbProvider = _dbBuilder.SetConnectionString(connectionString)
                                .SetDatabase("emonitor_db")
                                .SetCollection("books")
                                .Build();

            var books = await dbProvider.ShowAll<Book>();

            return books;
        }
    }
}
