using DataAccess;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ECommerceDbContext context) : base(context) { }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _context.Orders
        .Where(o => o.UserId == userId)
        .Include(o => o.Address)
        .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
        .ToListAsync();
    }
}
