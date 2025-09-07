using DataAccess;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(ECommerceDbContext context) : base(context) { }

    public async Task<IEnumerable<ProductImage>> GetImagesByProductIdAsync(int productId)
    {
        return await _context.ProductImages
                             .Where(pi => pi.ProductId == productId)
                             .ToListAsync();
    }
}
