using Euromonitor.Server.Interfaces.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Euromonitor.Server.MongoDbProvider.Models
{
    /// <summary>
    /// Represents book with name, text, purchace price etc.
    /// </summary>
    [Serializable]
    public class Book : IBook
    {
        /// <summary>
        /// Name of the book.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Text of the books.
        /// </summary>
        [BsonElement("text")]
        public string Text { get; set; }

        /// <summary>
        /// Purchase price of the book.
        /// </summary>
        [BsonElement("purchasePrice")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// URL to the book's cover.
        /// </summary>
        [BsonElement("coverUrl")]
        public string CoverUrl { get; set; }

        /// <summary>
        /// ISBN of the book.
        /// </summary>
        [BsonElement("isbn")]
        public string Isbn { get; set; }

        /// <summary>
        /// Overrides equals for Book instances.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Book book &&
                   Isbn == book.Isbn;
        }

        /// <summary>
        /// Retrieving hash code for book instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 2140778025 + EqualityComparer<string>.Default.GetHashCode(Isbn);
        }
    }
}
