using AutoMapper;
using ShopAPI.DTO.Order;
using ShopAPI.Models;
using ShopAPI.Repositories;

namespace ShopAPI.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetOrderDto>> GetOrdersAsync()
    {
        var orders = await _orderRepository.GetOrdersAsync();
        return _mapper.Map<IEnumerable<GetOrderDto>>(orders);
    }

    public async Task<GetOrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        return _mapper.Map<GetOrderDto>(order);
    }

    public async Task<GetOrderDto> AddOrderAsync(AddOrderDto addOrderDto)
    {
        var order = _mapper.Map<Order>(addOrderDto);
        
        order.Products = (await _productRepository.GetProductsByIdsAsync(addOrderDto.ProductIds.ToArray())).ToList();

        await _orderRepository.AddOrderAsync(order);
        return _mapper.Map<GetOrderDto>(order);
    }

    public async Task DeleteOrderAsync(int id)
    {
        await _orderRepository.DeleteOrderAsync(id);
    }
    
    public async Task UpdateOrderAsync(int id, AddOrderDto addOrderDto)
    {
        var order = _mapper.Map(addOrderDto, await _orderRepository.GetOrderByIdAsync(id));

        order.Products = (await _productRepository.GetProductsByIdsAsync(addOrderDto.ProductIds.ToArray())).ToList();

        await _orderRepository.UpdateOrderAsync(order);
    }

    public async Task AddProductsToOrderAsync(int orderId, int[] productIds)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        
        foreach (var product in await _productRepository.GetProductsByIdsAsync(productIds))
        {
            order.Products.Add(product);
        }

        await _orderRepository.UpdateOrderAsync(order);
    }
    
    public async Task RemoveProductsFromOrderAsync(int orderId, int[] productIds)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        
        foreach (var product in await _productRepository.GetProductsByIdsAsync(productIds))
        {
            order.Products.Remove(product);
        }

        await _orderRepository.UpdateOrderAsync(order);
    }
}