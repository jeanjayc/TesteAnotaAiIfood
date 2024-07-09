using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TesteAnotaAiIfood.Domain.Entities;
using TesteAnotaAiIfood.Infra.Data;
using TesteAnotaAiIfood.Infra.Interfaces;

namespace TesteAnotaAiIfood.Infra.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private IMongoCollection<Category> _categorysCollection;

        public CategoryRepository(IOptions<MongoDBSettings> settings)
        {
            var mongoClient = new MongoClient(
                settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                settings.Value.DatabaseName);

            _categorysCollection = mongoDatabase.GetCollection<Category>("category");
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categorysCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Category> GetById(string id)
        {
            var category = await _categorysCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return category;
        }
        public async Task<Category> InsertCategory(Category category)
        {
            await _categorysCollection.InsertOneAsync(category);
            return category;
        }
        public async Task UpdateCategory(string id, Category category)
        {

            await _categorysCollection.ReplaceOneAsync(c => c.Id == id, category);

        }
        public async Task DeleteCategory(string id)
        {
            var existCategory = await GetById(id);
            if (existCategory is null) return;

            await _categorysCollection.DeleteOneAsync(c => c.Id == id);
        }
    }
}
