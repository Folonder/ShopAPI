using Microsoft.EntityFrameworkCore;
using ShopAPI.Models;

namespace ShopAPI.Data;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Products)
            .WithMany(p => p.Orders)
            .UsingEntity<Dictionary<string, object>>(
                "orders_products",
                j => j
                    .HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("product_id"),
                j => j
                    .HasOne<Order>()
                    .WithMany()
                    .HasForeignKey("order_id")
            );
    }
}