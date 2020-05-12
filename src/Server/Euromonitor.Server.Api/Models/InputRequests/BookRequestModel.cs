namespace Euromonitor.Server.Api.Models
{
    /// <summary>
    /// Determines the book input request model to add it to the subscription.
    /// </summary>
    public class BookRequestModel
    {
        /// <summary>
        /// ISBN of the book.
        /// </summary>
        public string Isbn { get; set; }
    }
}
