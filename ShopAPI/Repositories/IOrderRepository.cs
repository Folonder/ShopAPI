using ShopAPI.Models;

namespace ShopAPI.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task AddOrderAsync(Order order);
    Task DeleteOrderAsync(int id);
    Task UpdateOrderAsync(Order order);
}   