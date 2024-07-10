using Microsoft.AspNetCore.Mvc;
using TesteAnotaAiIfood.Application.Interfaces;
using TesteAnotaAiIfood.Domain.DTOs;

namespace TesteAnotaAiIfood.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allProducts = await _productService.GetAllProducts();
                return Ok(allProducts);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            try
            {
                var response = await _productService.InsertProduct(productDTO);

                return CreatedAtAction(nameof(Create), productDTO);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, ProductDTO productDTO)
        {
            try
            {
                await _productService.UpdateProduct(id, productDTO);
                return NoContent();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
