using TesteAnotaAiIfood.Domain.DTOs;
using TesteAnotaAiIfood.Domain.Entities;

namespace TesteAnotaAiIfood.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> GetById(string id);
        Task<IEnumerable<CategoryDTO>> GetAllCategorys();
        Task<Category> InsertCategory(CategoryDTO categoryDTO);
        Task UpdateCategory(string id, CategoryDTO categoryDTO);
        Task DeleteCategory(string id);
    }
}
