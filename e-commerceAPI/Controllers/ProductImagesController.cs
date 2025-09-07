using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImagesController(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        // GET: api/productimages
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var images = await _productImageRepository.GetAllAsync();
            return Ok(images);
        }

        // GET: api/productimages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var image = await _productImageRepository.GetByIdAsync(id);
            if (image is null)
                return NotFound();

            return Ok(image);
        }

        // GET: api/productimages/product/3
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var images = await _productImageRepository.GetImagesByProductIdAsync(productId);
            return Ok(images);
        }

        // POST: api/productimages
        [HttpPost]
        public async Task<IActionResult> Create(ProductImage image)
        {
            await _productImageRepository.AddAsync(image);
            return CreatedAtAction(nameof(GetById), new { id = image.Id }, image);
        }

        // PUT: api/productimages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductImage updatedImage)
        {
            if (id != updatedImage.Id)
                return BadRequest();

            var existing = await _productImageRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _productImageRepository.UpdateAsync(updatedImage);
            return NoContent();
        }

        // DELETE: api/productimages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _productImageRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _productImageRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
