using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Catalog.Grpc.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ProductCode")]
        public string ProductCode { get; set; }
        [BsonElement("Stock")]
        public int Stock { get; set; }
        [BsonElement("Price")]
        public decimal Price { get; set; }
    }
}
