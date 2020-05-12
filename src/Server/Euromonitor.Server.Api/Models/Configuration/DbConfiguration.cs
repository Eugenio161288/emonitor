namespace Euromonitor.Server.Api.Models.Configuration
{
    /// <summary>
    /// Represents database configuration model from appsettings.
    /// </summary>
    public class DbConfiguration
    {
        /// <summary>
        /// Connection sting to the database.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Database name.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Users collection name.
        /// </summary>
        public string UsersCollection { get; set; }

        /// <summary>
        /// Books collection name.
        /// </summary>
        public string BooksCollection { get; set; }
    }
}
