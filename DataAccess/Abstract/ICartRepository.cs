using DataAccess.Abstract;

public interface ICartRepository : IRepository<Cart>
{
    Task<IEnumerable<CartItemDto>> GetByUserIdAsync(int userId);
    Task ClearCartAsync(int userId);
    Task IncreaseQuantityAsync(int userId, int productId, int amount);
    Task DecreaseQuantityAsync(int userId, int productId, int amount);
}
