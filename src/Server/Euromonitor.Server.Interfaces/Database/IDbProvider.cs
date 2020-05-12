using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euromonitor.Server.Interfaces.Database
{
    /// <summary>
    /// Represents database provider.
    /// </summary>
    public interface IDbProvider
    {
        /// <summary>
        /// The database value.
        /// </summary>
        string Database { get; }

        /// <summary>
        /// The collection value.
        /// </summary>
        string Collection { get; set; }

        /// <summary>
        /// The connection string to the database.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Inserts defined record to the database asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the record.</typeparam>
        /// <param name="record">The record value.</param>
        /// <returns></returns>
        Task InsertAsync<T>(T record);

        /// <summary>
        /// Updates specified record in database asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the record.</typeparam>
        /// <param name="record">The record value.</param>
        /// <returns></returns>
        Task UpdateAsync<T>(T record, string key, string value);

        /// <summary>
        /// Shows all records from collection asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<List<T>> ShowAll<T>();

        Task<T> FindRecord<T>(string key, string value);
    }
}
