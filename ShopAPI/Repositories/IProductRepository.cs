using ShopAPI.Models;

namespace ShopAPI.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task AddProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task UpdateProductAsync(Product product);
    Task<IEnumerable<Product>> GetProductsByIdsAsync(int[] productIds);
}