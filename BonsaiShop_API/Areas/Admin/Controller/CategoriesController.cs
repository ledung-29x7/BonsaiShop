using AutoMapper;
using BonsaiShop_API.Areas.Admin.Models;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonsaiShop_API.Areas.Admin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var category = await _categoryRepository.CreateCategoryAsync(categoryDto);
            var result = _mapper.Map<CategoryDto>(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            var result = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();
            var result = _mapper.Map<CategoryDto>(category);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(id, categoryDto);
            if (updatedCategory == null)
                return NotFound();
            var result = _mapper.Map<CategoryDto>(updatedCategory);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategoryAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

    }
}
