using Euromonitor.Server.Interfaces.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Euromonitor.Server.MongoDbProvider
{
    /// <summary>
    /// Builds MongoDbProvider instance with fluent interface.
    /// </summary>
    public class MongoDbProviderBuilder : IDbProviderBuilder
    {
        public IDbProvider Build()
        {
            throw new NotImplementedException();
        }

        public IDbProviderBuilder SetCollection(string collection)
        {
            throw new NotImplementedException();
        }

        public IDbProviderBuilder SetConnectionString(string connectionString)
        {
            throw new NotImplementedException();
        }

        public IDbProviderBuilder SetDatabase(string dbName)
        {
            throw new NotImplementedException();
        }
    }
}
