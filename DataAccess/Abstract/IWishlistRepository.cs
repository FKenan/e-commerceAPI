using DataAccess.Abstract;

public interface IWishlistRepository : IRepository<Wishlist>
{
    Task<IEnumerable<Wishlist>> GetByUserIdAsync(int userId);
}
    