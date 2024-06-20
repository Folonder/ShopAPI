using Microsoft.EntityFrameworkCore;
using ShopAPI.Data;
using ShopAPI.Exceptions;
using ShopAPI.Models;

namespace ShopAPI.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ShopContext _context;

    public OrderRepository(ShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _context.Orders.Include(o => o.Products).ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _context.Orders.Include(o => o.Products)
            .FirstOrDefaultAsync(o => o.OrderId == id) ??
            throw new NotFoundException($"Order with id {id} is not found.");
    }

    public async Task AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id) ??
                    throw new NotFoundException($"Order with id {id} is not found.");
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}