using DataAccess.Abstract;

public interface IAddressRepository : IRepository<Address>
{
    Task<IEnumerable<Address>> GetAddressesByUserIdAsync(int userId);
}
