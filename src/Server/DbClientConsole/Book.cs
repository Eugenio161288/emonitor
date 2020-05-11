using Euromonitor.Server.Interfaces.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DbClientConsole
{
    [Serializable]
    public class Book : IBook
    {
        //[DataMemeber]
        [BsonElement("name")]
        public string Name { get; set; }

        //[BsonId]
        //public ObjectId MongoId { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("purchasePrice")]
        public Decimal PurchasePrice { get; set; }
    }
}
