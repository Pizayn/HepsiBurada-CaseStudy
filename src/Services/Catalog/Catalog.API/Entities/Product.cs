using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ProductCode")]
        [BsonRequired]
        public string ProductCode { get; set; }
        [BsonElement("Stock")]
        [BsonRequired]

        public int Stock { get; set; }
        [BsonElement("Price")]
        [BsonRequired]

        public double Price { get; set; }
    }
}
