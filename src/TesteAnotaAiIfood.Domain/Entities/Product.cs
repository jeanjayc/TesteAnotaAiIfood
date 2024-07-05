using MongoDB.Bson.Serialization.Attributes;

namespace TesteAnotaAiIfood.Domain.Entities
{
    public class Product: BaseEntity
    {
        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("categoria")]
        public Category Categoria { get; set; }
    }
}
