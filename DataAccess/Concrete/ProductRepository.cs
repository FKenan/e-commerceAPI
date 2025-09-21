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
}
