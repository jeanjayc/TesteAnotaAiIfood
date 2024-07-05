using TesteAnotaAiIfood.Domain.Entities;

namespace TesteAnotaAiIfood.Infra.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> InsertProduct(Product product);
        Task<Product> UpdateProduct(string id, Product product);
        Task DeleteProduct(string id);
    }
}
