using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        // GET: api/categories/slug/electronics
        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var category = await _categoryRepository.GetBySlugAsync(slug);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _categoryRepository.AddAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category updatedCategory)
        {
            if (id != updatedCategory.Id)
                return BadRequest();

            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _categoryRepository.UpdateAsync(updatedCategory);
            return NoContent();
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _categoryRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
