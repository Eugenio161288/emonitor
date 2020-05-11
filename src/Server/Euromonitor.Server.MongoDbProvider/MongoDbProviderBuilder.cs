using Euromonitor.Server.Interfaces.Database;

namespace Euromonitor.Server.MongoDbProvider
{
    /// <summary>
    /// Builds MongoDbProvider instance with fluent interface.
    /// </summary>
    public class MongoDbProviderBuilder : IDbProviderBuilder
    {
        MongoDbProvider _dbProvider = new MongoDbProvider();

        /// <summary>
        /// Initializes an instance of the MongoDbProviderBuilder.
        /// </summary>
        public MongoDbProviderBuilder()
        {}

        /// <summary>
        /// Builds MongoDbProvider instance with initializes database name and connection string.
        /// </summary>
        /// <returns></returns>
        public IDbProvider Build()
        {
            _dbProvider.Initialize();

            return _dbProvider;
        }

        /// <summary>
        /// Sets collection name to the db provider.
        /// </summary>
        /// <param name="collection">The collection name.</param>
        /// <returns>Returns MongoDbProviderBuilder instance with defined collection name for provider.</returns>
        public IDbProviderBuilder SetCollection(string collection)
        {
            if (!string.IsNullOrEmpty(collection))
            {
                _dbProvider.Collection = collection;
            }

            return this;
        }

        /// <summary>
        /// Sets connection string to the db provider.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Returns MongoDbProviderBuilder instance with defined connection string.</returns>
        public IDbProviderBuilder SetConnectionString(string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                _dbProvider.ConnectionString = connectionString;
            }

            return this;
        }

        /// <summary>
        /// Sets db name to the db provider.
        /// </summary>
        /// <param name="dbName">The dtabase name.</param>
        /// <returns></returns>
        public IDbProviderBuilder SetDatabase(string dbName)
        {
            if (!string.IsNullOrEmpty(dbName))
            {
                _dbProvider.Database = dbName;
            }

            return this;
        }
    }
}
