using Entities;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        // GET: api/products/5 => {id} tanımı ?id=5 yerine /5 şeklinde kullanılmasını sağlar.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
                return NotFound();

            return Ok(product);
        }

        // GET: api/products/by-category/3
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            return Ok(products);
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product updatedProduct)
        {
            if (id != updatedProduct.Id)
                return BadRequest();

            var existing = await _productRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _productRepository.UpdateAsync(updatedProduct);
            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _productRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
