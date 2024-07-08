﻿using Microsoft.AspNetCore.Mvc;
using TesteAnotaAiIfood.Application.Interfaces;
using TesteAnotaAiIfood.Domain.DTOs;

namespace TesteAnotaAiIfood.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            try
            {
                var result = await _categoryService.GetById(id);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return NotFound();
            }

            var response = await _categoryService.InsertCategory(categoryDTO);

            return Ok();
        }
    }
}
