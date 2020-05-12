using System;

namespace Euromonitor.Server.Interfaces.Entities
{
    /// <summary>
    /// Represents book's properties like title, price etc.
    /// </summary>
    public interface IBook
    {
        /// <summary>
        /// The title of the book.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The content of the book.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// The purchase price of the book.
        /// </summary>
        Decimal PurchasePrice { get; set; }

        /// <summary>
        /// The URL of the cover.
        /// </summary>
        string CoverUrl { get; set; }

        /// <summary>
        /// ISBN of the book.
        /// </summary>
        string Isbn { get; set; }
    }
}
