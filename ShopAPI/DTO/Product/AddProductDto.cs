namespace ShopAPI.DTO.Product;

public class AddProductDto
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    
    public override string ToString()
    {
        return $"Name: {Name}, Price: {Price}";
    }
}