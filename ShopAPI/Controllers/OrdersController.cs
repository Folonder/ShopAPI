using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTO.Order;
using ShopAPI.Services;

namespace ShopAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetOrderDto>>> GetOrders()
    {
        _logger.LogInformation("Getting all orders");
        var orders = await _orderService.GetOrdersAsync();
        _logger.LogInformation($"Got orders: {string.Join(", ", orders)}");
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetOrderDto>> GetOrder(int id)
    {
        _logger.LogInformation($"Getting order with id: {id}");
        var order = await _orderService.GetOrderByIdAsync(id);
        _logger.LogInformation($"Got order with id: {order.OrderId}");
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<GetOrderDto>> PostOrder(AddOrderDto addOrderDto)
    {
        _logger.LogInformation($"Posting order: {addOrderDto}");
        var getOrderDto = await _orderService.AddOrderAsync(addOrderDto);
        _logger.LogInformation($"Added order: {getOrderDto}");
        return CreatedAtAction(nameof(GetOrder), new { id = getOrderDto.OrderId }, getOrderDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        _logger.LogInformation($"Deleting Order with id: {id}");
        await _orderService.DeleteOrderAsync(id);
        _logger.LogInformation($"Deleted order with id: {id}");
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, AddOrderDto updateOrderDto)
    {
        _logger.LogInformation($"Updating order: {updateOrderDto} with id: {id}");
        await _orderService.UpdateOrderAsync(id, updateOrderDto);
        _logger.LogInformation($"Updated order with id: {id}");
        return Ok();
    }

    [HttpPut("{id}/remove-products")]
    public async Task<IActionResult> RemoveProductsFromOrder(int id, [FromBody] int[] productIds)
    {
        _logger.LogInformation($"Removing products with ids: {productIds} from order with id: {id}");
        await _orderService.RemoveProductsFromOrderAsync(id, productIds);
        _logger.LogInformation($"Removed products with ids: {productIds} from order with id: {id}");
        return Ok();
    }
    
    [HttpPut("{id}/add-products")]
    public async Task<IActionResult> AddProductsToOrder(int id, [FromBody] int[] productIds)
    {
        _logger.LogInformation($"Adding products with ids: {productIds} from order with id: {id}");
        await _orderService.AddProductsToOrderAsync(id, productIds);
        _logger.LogInformation($"Add products with ids: {productIds} from order with id: {id}");
        return Ok();
    }
}
