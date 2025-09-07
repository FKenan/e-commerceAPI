using DataAccess.Abstract;

public interface IProductImageRepository : IRepository<ProductImage>
{
    Task<IEnumerable<ProductImage>> GetImagesByProductIdAsync(int productId);
}
