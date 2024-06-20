namespace ShopAPI.DTO.Order;

public class AddOrderDto
{
    public DateTime OrderDate { get; set; }
    public HashSet<int> ProductIds { get; set; } = null!;
    
    public override string ToString()
    {
        return $"OrderDate: {OrderDate}, ProductIds: {string.Join(", ", ProductIds)}";
    }
}