using DataAccess;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ECommerceDbContext context) : base(context) { }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _context.Orders
                             .Include(o => o.OrderItems)
                             .Where(o => o.UserId == userId)
                             .ToListAsync();
    }
}
