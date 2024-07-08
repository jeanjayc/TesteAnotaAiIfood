using TesteAnotaAiIfood.Application.Interfaces;
using TesteAnotaAiIfood.Domain.DTOs;
using TesteAnotaAiIfood.Domain.Entities;
using TesteAnotaAiIfood.Infra.Interfaces;

namespace TesteAnotaAiIfood.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Product> InsertProduct(ProductDTO productDTO)
        {
            var category = await _categoryRepository.GetById(productDTO.CategoryId);

            if(category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var newProduct = new Product(productDTO);

            newProduct.Categoria = category;

            var registeredProduct = await _productRepository.InsertProduct(newProduct);

            return registeredProduct;
        }

        public Task<Product> UpdateProduct(string id, ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }
    }
}
