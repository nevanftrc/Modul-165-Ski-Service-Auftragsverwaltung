using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SkiServiceAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string UserName { get; set; }

        [BsonRequired]
        public string Password { get; set; }
    }
}
