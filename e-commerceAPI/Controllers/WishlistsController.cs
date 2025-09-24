using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController : ControllerBase
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistsController(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        // GET: api/wishlists
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var wishlists = await _wishlistRepository.GetAllAsync();
            return Ok(wishlists);
        }

        // GET: api/wishlists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var wishlist = await _wishlistRepository.GetByIdAsync(id);
            if (wishlist is null)
                return NotFound();

            return Ok(wishlist);
        }

        // GET: api/wishlists/user/3
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var wishlists = await _wishlistRepository.GetByUserIdAsync(userId);
            return Ok(wishlists);
        }

        // POST: api/wishlists
        [HttpPost]
        public async Task<IActionResult> Create(WishlistRequest wishlistRequest)
        {
            var wishlist = new Wishlist
            {
                UserId = wishlistRequest.UserId,
                ProductId = wishlistRequest.ProductId
            };

            await _wishlistRepository.AddAsync(wishlist);
            return CreatedAtAction(nameof(GetById), new { id = wishlist.Id }, wishlist);
        }

        // DELETE: api/wishlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _wishlistRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _wishlistRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
