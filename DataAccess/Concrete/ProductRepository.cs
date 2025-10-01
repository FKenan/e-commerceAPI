using DataAccess;
using DataAccess.Concrete;
using Entities;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ECommerceDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetWithCategoryAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
    {
        return await _context.Products
            //.Include(p => p.ProductImages)
            .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                MainImageUrl = null /*p.ProductImages.FirstOrDefault(i => i.IsMain)?.ImageUrl  => images not implemented yet*/
            })
            .ToListAsync();
    }
}
