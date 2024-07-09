using TesteAnotaAiIfood.Domain.Entities;

namespace TesteAnotaAiIfood.Infra.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetById(string id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> InsertProduct(Product product);
        Task UpdateProduct(string id, Product product);
        Task DeleteProduct(string id);
    }
}
