namespace Euromonitor.Server.Interfaces.Database
{
    /// <summary>
    /// Represents database provider builder with fluent interface.
    /// </summary>
    public interface IDbProviderBuilder
    {
        /// <summary>
        /// Sets database for db provider.
        /// </summary>
        /// <param name="dbName">The database name.</param>
        /// <returns>Returns db provider with defined db name.</returns>
        IDbProviderBuilder SetDatabase(string dbName);

        /// <summary>
        /// Sets database collection for db provider.
        /// </summary>
        /// <param name="collection">The database collection name.</param>
        /// <returns>Returns db provider with defined collection name.</returns>
        IDbProviderBuilder SetCollection(string collection);

        /// <summary>
        /// Sets connection string to the database for db provider.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <returns>Returns db provider with defined connection string.</returns>
        IDbProviderBuilder SetConnectionString(string connectionString);

        /// <summary>
        /// Builds db provider with defined properties like db name, connection string etc.
        /// </summary>
        /// <returns>Returns the cref=IDbProvider </returns>
        IDbProvider Build();
    }
}
