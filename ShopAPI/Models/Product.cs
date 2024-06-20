using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPI.Models;

[Table("products")]
public class Product
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("product_id")]
    public int ProductId { get; set; }
    
    [Column("name_id")] 
    public string Name { get; set; } = null!;

    [Column("price_id")]
    public decimal Price { get; set; }
    
    [Column("order_id")]
    public ICollection<Order> Orders { get; set; } = null!;
}