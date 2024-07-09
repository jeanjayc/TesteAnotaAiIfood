using MongoDB.Bson.Serialization.Attributes;
using TesteAnotaAiIfood.Domain.DTOs;

namespace TesteAnotaAiIfood.Domain.Entities
{
    public class Product: BaseEntity
    {
        public Product(ProductDTO productDTO)
        {
            Title = productDTO.Title;
            Owner = productDTO.Owner;
            Price = productDTO.Price;
            Description = productDTO.Description;
        }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("categoria")]
        public Category? Categoria { get; set; }
    }
}
