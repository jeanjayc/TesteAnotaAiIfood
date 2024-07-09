using TesteAnotaAiIfood.Domain.DTOs;
using TesteAnotaAiIfood.Domain.Entities;

namespace TesteAnotaAiIfood.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<Product> InsertProduct(ProductDTO productDTO);
        Task UpdateProduct(string id, ProductDTO productDTO);
        Task DeleteProduct(string id);
    }
}
