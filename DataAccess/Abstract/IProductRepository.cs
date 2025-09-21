using DataAccess.Abstract;
using Entities;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetWithCategoryAsync();
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
}
