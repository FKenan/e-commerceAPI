using DataAccess.Abstract;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetWithCategoryAsync();
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
    Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
}
