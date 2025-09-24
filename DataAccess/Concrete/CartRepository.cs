using DataAccess;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(ECommerceDbContext context) : base(context) { }

    public async Task<IEnumerable<CartItemDto>> GetByUserIdAsync(int userId)
    {
        return await _context.Carts
                             .Include(c => c.Product)
                             .Where(c => c.UserId == userId)
                             .Select(c => new CartItemDto
                             {
                                 ProductId = c.ProductId,
                                 ProductName = c.Product.Name,
                                 Quantity = c.Quantity,
                                 Price = c.Product.Price
                             })
                             .ToListAsync();
    }

    public async Task ClearCartAsync(int userId)
    {
        var carts = await _context.Carts.Where(c => c.UserId == userId).ToListAsync();
        _context.Carts.RemoveRange(carts);
        await _context.SaveChangesAsync();
    }

    public async Task IncreaseQuantityAsync(int userId, int productId, int amount)
    {
        var cartItem = await _context.Carts
                                     .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        if (cartItem is null)
        {
            cartItem = new Cart
            {
                UserId = userId,
                ProductId = productId,
                Quantity = amount,
            };
            await _context.Carts.AddAsync(cartItem);
        }
        else
        {
            cartItem.Quantity += amount;
            _context.Carts.Update(cartItem);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DecreaseQuantityAsync(int userId, int productId, int amount)
    {
        var cartItem = await _context.Carts
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        if (cartItem != null)
        {
            cartItem.Quantity -= amount;
            if (cartItem.Quantity <= 0)
            {
                _context.Carts.Remove(cartItem);
            }
            else
            {
                _context.Carts.Update(cartItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
