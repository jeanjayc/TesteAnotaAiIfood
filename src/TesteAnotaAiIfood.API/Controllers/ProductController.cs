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

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            try
            {
                var response = await _productService.InsertProduct(productDTO);

                return Ok(productDTO);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, ProductDTO productDTO)
        {

            return Ok();
        }
    }
}
