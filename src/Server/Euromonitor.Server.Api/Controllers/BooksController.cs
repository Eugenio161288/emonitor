using Euromonitor.Server.Api.Models.Configuration;
using Euromonitor.Server.Interfaces.Database;
using Euromonitor.Server.MongoDbProvider.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euromonitor.Server.Api.Controllers
{
    /// <summary>
    /// Determines REST API methods for books.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IDbProviderBuilder _dbBuilder;

        private DbConfiguration _dbConfiguration;

        public BooksController(IDbProviderBuilder dbProviderBuilder, IConfiguration configuration)
        {
            _dbBuilder = dbProviderBuilder;
            _dbConfiguration = configuration.GetSection("databaseSettings").Get<DbConfiguration>();
        }

        /// <summary>
        /// Shows all books.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Book>> ShowAll()
        {
            var dbProvider = _dbBuilder.SetConnectionString(_dbConfiguration.ConnectionString)
                                .SetDatabase(_dbConfiguration.Database)
                                .SetCollection(_dbConfiguration.BooksCollection)
                                .Build();

            var books = await dbProvider.ShowAll<Book>();

            return books;
        }
    }
}
