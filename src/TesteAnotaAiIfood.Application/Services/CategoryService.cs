using TesteAnotaAiIfood.Application.Interfaces;
using TesteAnotaAiIfood.Domain.DTOs;
using TesteAnotaAiIfood.Domain.Entities;
using TesteAnotaAiIfood.Infra.Interfaces;

namespace TesteAnotaAiIfood.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAwsService _awsService;
        public CategoryService(ICategoryRepository categoryRepository, IAwsService awsService)
        {
            _categoryRepository = categoryRepository;
            _awsService = awsService;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategorys()
        {
            var categorys = await _categoryRepository.GetAll();
            return categorys.Select
                (
                    c => new CategoryDTO
                        (
                            c.Owner,
                            c.Description,
                            c.Title,
                            c.ProductId
                        )
                );
        }
        public async Task<CategoryDTO> GetById(string id)
        {
            var category = await _categoryRepository.GetById(id);

            return new CategoryDTO
                (
                    category.Id,
                    category.Title,
                    category.Description,
                    category.ProductId
                );
        }
        public async Task<Category> InsertCategory(CategoryDTO categoryDTO)
        {
            var newCategory = new Category(categoryDTO);
            var registeredCategory = await _categoryRepository.InsertCategory(newCategory);
            await _awsService.PublishToTopic(registeredCategory.Owner);
            return registeredCategory;
        }
        public async Task UpdateCategory(string id, CategoryDTO categoryDTO)
        {
            var existCategory = await _categoryRepository.GetById(id);

            if (existCategory is null) return;

            var updateCategory = new Category(categoryDTO);
            updateCategory.Id = existCategory.Id;

            await _categoryRepository.UpdateCategory(id, updateCategory);
            await _awsService.PublishToTopic(updateCategory.Owner);
        }

        public async Task DeleteCategory(string id)
        {
            var existCategory = await _categoryRepository.GetById(id);

            if (existCategory is null) return;

            await _categoryRepository.DeleteCategory(id);
        }
    }
}
