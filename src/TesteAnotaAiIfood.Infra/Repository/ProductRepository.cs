using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TesteAnotaAiIfood.Domain.Entities;
using TesteAnotaAiIfood.Infra.Data;
using TesteAnotaAiIfood.Infra.Interfaces;

namespace TesteAnotaAiIfood.Infra.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IMongoCollection<Product> _productsCollection;

        public ProductRepository(IOptions<MongoDBSettings> settings)
        {
            var mongoClient = new MongoClient(
                settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                settings.Value.DatabaseName );

            _productsCollection = mongoDatabase.GetCollection<Product>(settings.Value.DatabaseName);
        }

        public async Task<Product> GetById(string id)
        {
            return await _productsCollection.Find(id).FirstOrDefaultAsync();
        }

        public async Task<Product> InsertProduct(Product product)
        {
            await _productsCollection.InsertOneAsync(product);
            return product;
        }
        public async Task UpdateProduct(string id, Product updateProduct)
        {
            var product = await GetById(id);

            if (product is null) return;

            await _productsCollection.ReplaceOneAsync(p => p.Id == id, updateProduct);
        }

        public async Task DeleteProduct(string id)
        {
            var product = await GetById(id);

            if (product is null) return;

            await _productsCollection.DeleteOneAsync(p => p.Id == id);
        }
    }
}
