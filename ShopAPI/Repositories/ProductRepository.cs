using Microsoft.EntityFrameworkCore;
using ShopAPI.Data;
using ShopAPI.Exceptions;
using ShopAPI.Models;

namespace ShopAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShopContext _context;

    public ProductRepository(ShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id) ?? 
               throw new NotFoundException($"Product with id {id} is not found.");
    }

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await GetProductByIdAsync(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByIdsAsync(int[] productIds)
    {
        var products = await _context.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync();
        
        if (productIds.Length != products.Count)
        {
            throw new NotFoundException("One or more products are not found.");
        }

        return products;
    }
    
}