using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TimeService.API.Entities
{
    public class Time
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Hour { get; set; }
    }
}
