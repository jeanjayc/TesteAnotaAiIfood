using TesteAnotaAiIfood.Application.Interfaces;
using TesteAnotaAiIfood.Domain.DTOs;
using TesteAnotaAiIfood.Domain.Entities;
using TesteAnotaAiIfood.Infra.Interfaces;

namespace TesteAnotaAiIfood.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryDTO> GetById(string id)
        {
            var category = await _categoryRepository.GetById(id);

            return new CategoryDTO
                (
                    category.Id, 
                    category.Title, 
                    category.Description
                );
        }
        public async Task<Category> InsertCategory(CategoryDTO categoryDTO)
        {
            var newCategory = new Category(categoryDTO);
            var registeredCategory = await _categoryRepository.InsertCategory(newCategory);
            return registeredCategory;
        }

        public Task DeleteCategory(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategory(string id, CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
