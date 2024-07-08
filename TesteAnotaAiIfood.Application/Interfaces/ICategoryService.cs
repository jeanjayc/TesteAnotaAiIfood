using TesteAnotaAiIfood.Domain.DTOs;
using TesteAnotaAiIfood.Domain.Entities;

namespace TesteAnotaAiIfood.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> GetById(string id);
        Task<Category> InsertCategory(CategoryDTO categoryDTO);
        Task<Category> UpdateCategory(string id, CategoryDTO categoryDTO);
        Task DeleteCategory(string id);
    }
}
