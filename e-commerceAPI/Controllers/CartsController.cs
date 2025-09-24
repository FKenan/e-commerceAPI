using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly ICartRepository _cartRepository;

    public CartsController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    // GET: api/carts/3
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var carts = await _cartRepository.GetByUserIdAsync(userId);
        return Ok(carts);
    }

    // DELETE: api/carts/clear/3
    [HttpDelete("clear/{userId}")]
    public async Task<IActionResult> ClearCart(int userId)
    {
        await _cartRepository.ClearCartAsync(userId);
        return NoContent();
    }

    // POST: api/carts/increase
    [HttpPost("increase")]
    public async Task<IActionResult> IncreaseQuantity([FromBody] CartQuantityRequest request)
    {
        await _cartRepository.IncreaseQuantityAsync(request.UserId, request.ProductId, request.Amount);
        return Ok("Quantity updated.");
    }

    // POST: api/carts/decrease
    [HttpPost("decrease")]
    public async Task<IActionResult> DecreaseQuantity([FromBody] CartQuantityRequest request)
    {
        await _cartRepository.DecreaseQuantityAsync(request.UserId, request.ProductId, request.Amount);
        return Ok("Quantity decreased or item removed.");
    }
}
