using Microsoft.AspNetCore.Mvc;
using DataAccess;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public SearchController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("{searchTerm}")]
    public async Task<IActionResult> Search(string searchTerm)
    {
        var results = await _productRepository.SearchProductsAsync(searchTerm);
        return Ok(results);
    }
}