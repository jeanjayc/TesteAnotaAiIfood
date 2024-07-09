using TesteAnotaAiIfood.Domain.Entities;

namespace TesteAnotaAiIfood.Infra.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetById(string id);
        Task<IEnumerable<Category>> GetAll();
        Task<Category> InsertCategory(Category category);
        Task UpdateCategory(string id, Category category);
        Task DeleteCategory(string id);
    }
}
