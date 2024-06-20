using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTO.Product;
using ShopAPI.Services;

namespace ShopAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetProductDto>>> GetProducts()
    {
        _logger.LogInformation($"Getting all products");
        var products = await _productService.GetProductsAsync();
        _logger.LogInformation($"Got products: {string.Join(", ", products)}");
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetProductDto>> GetProduct(int id)
    {
        _logger.LogInformation($"Getting product with id: {id}");
        var product = await _productService.GetProductByIdAsync(id);
        _logger.LogInformation($"Got product with id: {product.ProductId}");
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<GetProductDto>> PostProduct(AddProductDto addProductDto)
    {
        _logger.LogInformation($"Adding product: {addProductDto}");
        var getProductDto = await _productService.AddProductAsync(addProductDto);
        _logger.LogInformation($"Added product: {getProductDto}");
        return CreatedAtAction(nameof(GetProduct), new { id = getProductDto.ProductId }, getProductDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        _logger.LogInformation($"Deleting product with id: {id}");
        await _productService.DeleteProductAsync(id);
        _logger.LogInformation($"Deleted product with id: {id}");
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] AddProductDto updateProductDto)
    {
        _logger.LogInformation($"Updating product: {updateProductDto} with id: {id}");
        await _productService.UpdateProductAsync(id, updateProductDto);
        _logger.LogInformation($"Updated product with id: {id}");
        return Ok();
    }
}