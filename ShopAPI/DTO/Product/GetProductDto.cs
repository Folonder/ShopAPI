namespace ShopAPI.DTO.Product;

public class GetProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    
    public override string ToString()
    {
        return $"Id: {ProductId}, Name: {Name}, Price: {Price}";
    }
}