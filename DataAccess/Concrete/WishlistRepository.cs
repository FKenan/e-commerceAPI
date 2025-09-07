using DataAccess;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
{
    public WishlistRepository(ECommerceDbContext context) : base(context) { }

    public async Task<IEnumerable<Wishlist>> GetByUserIdAsync(int userId)
    {
        return await _context.Wishlists
                             .Include(w => w.Product)
                             .Where(w => w.UserId == userId)
                             .ToListAsync();
    }
}
