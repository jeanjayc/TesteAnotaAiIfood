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
        private readonly IAwsService _awsService;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IAwsService awsService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _awsService = awsService;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return products.Select
                (
                    c => new ProductDTO
                        (
                            c.Title,
                            c.Owner,
                            c.Categoria.Id,
                            c.Price,
                            c.Description
                        )
                );
        }
        public async Task<Product> InsertProduct(ProductDTO productDTO)
        {
            var category = await _categoryRepository.GetById(productDTO.CategoryId);

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if(!string.IsNullOrWhiteSpace(category.ProductId))
            {
                throw new ArgumentException("A categoria já está vinculada a outro produto");
            }

            var newProduct = new Product(productDTO);

            newProduct.Categoria = category;

            var registeredProduct = await _productRepository.InsertProduct(newProduct);
            await _awsService.PublishToTopic(registeredProduct.Owner);

            category.ProductId = registeredProduct.Id;
            await _categoryRepository.UpdateCategory(category.Id, category);

            return registeredProduct;
        }

        public async Task UpdateProduct(string id, ProductDTO product)
        {
            var productExist = await _productRepository.GetById(id);
            if (productExist is null) return;

            var newProduct = new Product(product);
            newProduct.Id = productExist.Id;

            await _productRepository.UpdateProduct(id, newProduct);
            await _awsService.PublishToTopic(product.Owner);
        }

        public async Task DeleteProduct(string id)
        {
            var productExist = await _productRepository.GetById(id);
            if (productExist is null) return;

            await _productRepository.DeleteProduct(id);
        }
    }
}
