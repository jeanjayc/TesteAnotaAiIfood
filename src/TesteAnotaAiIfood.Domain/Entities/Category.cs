using TesteAnotaAiIfood.Domain.DTOs;

namespace TesteAnotaAiIfood.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category(CategoryDTO categoryDTO)
        {
            Title = categoryDTO.Title;
            Owner = categoryDTO.Owner;
            Description = categoryDTO.Description;
            ProductId = string.Empty;
        }
        public string ProductId { get; set; }
    }
}
