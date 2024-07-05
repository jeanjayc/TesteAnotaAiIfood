using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TesteAnotaAiIfood.Domain.Entities;
using TesteAnotaAiIfood.Infra.Data;
using TesteAnotaAiIfood.Infra.Interfaces;

namespace TesteAnotaAiIfood.Infra.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private IMongoCollection<Product> _products;

        public ProductRepository(IMongoClient client, IOptions<MongoDBSettings> settings, IMongoCollection<Product> products)
        {
            _client = client;
            _database = _client.GetDatabase(settings.Value.DatabaseName);
            _products = _database.GetCollection<Product>("produto");
        }

        public async Task<Product> InsertProduct(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }
        public async Task<Product> UpdateProduct(string id, Product product)
        {
            await _products.ReplaceOneAsync(x => x.Id == id, product);
            return product;
        }

        public async Task DeleteProduct(string id)
        {
            await _products.DeleteOneAsync(x => x.Id == id);
        }
    }
}
