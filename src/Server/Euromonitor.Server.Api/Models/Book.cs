using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Euromonitor.Server.Api.Models
{
    [Serializable]
    public class Book
    {
        //[DataMemeber]
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("purchasePrice")]
        public string PurchasePrice { get; set; }

        [BsonElement("coverUrl")]
        public string CoverUrl { get; set; }

        //public bool IsSubscribed { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Book book &&
                   Name == book.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
