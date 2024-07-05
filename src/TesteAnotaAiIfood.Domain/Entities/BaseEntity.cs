using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TesteAnotaAiIfood.Domain.Entities
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("owner")]
        public string Owner { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}
