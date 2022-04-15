using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Product.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ProductCode")]
        public string ProductCode { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
