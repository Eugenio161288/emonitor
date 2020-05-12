using Euromonitor.Server.Api.Models.Data;
using Euromonitor.Server.Interfaces.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euromonitor.Server.Api.Controllers
{
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

        [HttpGet]
        public async Task<List<Book>> Show()
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
