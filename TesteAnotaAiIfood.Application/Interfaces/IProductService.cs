using TesteAnotaAiIfood.Domain.DTOs;
using TesteAnotaAiIfood.Domain.Entities;

namespace TesteAnotaAiIfood.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> InsertProduct(ProductDTO productDTO);
        Task<Product> UpdateProduct(string id, ProductDTO productDTO);
        Task DeleteProduct(string id);
    }
}
