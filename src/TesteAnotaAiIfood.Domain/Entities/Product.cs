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
        public decimal Price { get; set; }
        public Category? Categoria { get; set; }
    }
}
