using DataAccess.Abstract;
using Entities;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetWithCategoryAsync();
}
