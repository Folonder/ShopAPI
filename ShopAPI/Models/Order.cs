using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPI.Models;

[Table("orders")]
public class Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("order_id")]
    public int OrderId { get; set; }
    [Column("order_date")]
    public DateTime OrderDate { get; set; }
    [Column("product_id")]
    public ICollection<Product> Products { get; set; } = null!;
}