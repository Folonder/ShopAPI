using ShopAPI.DTO.Product;

namespace ShopAPI.DTO.Order;

public class GetOrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public GetProductDto[] Products { get; set; } = null!;
    
    public override string ToString()
    {
        return $"Id: {OrderId}, OrderDate: {OrderDate}, Products: {string.Join(", ", (IEnumerable<GetProductDto>) Products)}";
    }
}