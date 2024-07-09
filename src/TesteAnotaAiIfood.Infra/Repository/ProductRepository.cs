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

            _productsCollection = mongoDatabase.GetCollection<Product>("product");
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productsCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Product> GetById(string id)
        {
            return await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> InsertProduct(Product product)
        {
            await _productsCollection.InsertOneAsync(product);
            return product;
        }
        public async Task UpdateProduct(string id, Product updateProduct)
        {
            await _productsCollection.ReplaceOneAsync(p => p.Id == id, updateProduct);
        }

        public async Task DeleteProduct(string id)
        {
            await _productsCollection.DeleteOneAsync(p => p.Id == id);
        }
    }
}
