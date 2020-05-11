using Euromonitor.Server.Interfaces.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbClientConsole
{
    public class MongoDbProviderBuilder : IDbProviderBuilder
    {
        MongoDbProvider dbProvider = new MongoDbProvider();

        public IDbProvider Build()
        {
            dbProvider.Build();

            return dbProvider;
        }

        public IDbProviderBuilder SetCollection(string collection)
        {
            if(!string.IsNullOrEmpty(collection))
            {
                dbProvider.Collection = collection;
            }

            return this;
        }

        public IDbProviderBuilder SetConnectionString(string connectionString)
        {
            if(!string.IsNullOrEmpty(connectionString))
            {
                dbProvider.ConnectionString = connectionString;
            }

            return this;
        }

        public IDbProviderBuilder SetDatabase(string dbName)
        {
            if(!string.IsNullOrEmpty(dbName))
            {
                dbProvider.Database = dbName;
            }

            return this;
        }
    }
}
