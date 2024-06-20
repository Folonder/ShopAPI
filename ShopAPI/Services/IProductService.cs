using ShopAPI.DTO.Product;

namespace ShopAPI.Services;

public interface IProductService
{
    Task<IEnumerable<GetProductDto>> GetProductsAsync();
    Task<GetProductDto> GetProductByIdAsync(int id);
    Task<GetProductDto> AddProductAsync(AddProductDto productDto);
    Task DeleteProductAsync(int id);
    Task UpdateProductAsync(int id, AddProductDto productDto);
}