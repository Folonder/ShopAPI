using ShopAPI.DTO.Order;

namespace ShopAPI.Services;

public interface IOrderService
{
    Task<IEnumerable<GetOrderDto>> GetOrdersAsync();
    Task<GetOrderDto> GetOrderByIdAsync(int id);
    Task<GetOrderDto> AddOrderAsync(AddOrderDto addOrderDto);
    Task DeleteOrderAsync(int id);
    Task AddProductsToOrderAsync(int orderId, int[] productIds);
    Task RemoveProductsFromOrderAsync(int orderId, int[] productIds);
    Task UpdateOrderAsync(int id, AddOrderDto addOrderDto);
}