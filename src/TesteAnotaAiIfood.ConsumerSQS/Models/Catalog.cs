using TesteAnotaAiIfood.Domain.DTOs;

namespace TesteAnotaAiIfood.Servesless.Models
{
    public class Catalog
    {
        public string Owner { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryDescription { get; set; }
        public List<ProductDTO> Itens { get; set; }
    }
}
