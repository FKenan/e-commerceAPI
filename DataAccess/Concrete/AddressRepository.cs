using DataAccess;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(ECommerceDbContext context) : base(context) { }

    public async Task<IEnumerable<Address>> GetAddressesByUserIdAsync(int userId)
    {
        return await _context.Addresses
                             .Where(a => a.UserId == userId)
                             .ToListAsync();
    }
}
