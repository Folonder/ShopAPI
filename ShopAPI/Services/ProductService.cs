using AutoMapper;
using ShopAPI.DTO.Product;
using ShopAPI.Models;
using ShopAPI.Repositories;

namespace ShopAPI.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetProductDto>> GetProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();
        return _mapper.Map<IEnumerable<GetProductDto>>(products);
    }

    public async Task<GetProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return _mapper.Map<GetProductDto>(product);
    }

    public async Task<GetProductDto> AddProductAsync(AddProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _productRepository.AddProductAsync(product);
        return _mapper.Map<GetProductDto>(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteProductAsync(id);
    }
    
    public async Task UpdateProductAsync(int id, AddProductDto productDto)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        _mapper.Map(productDto, product);
        await _productRepository.UpdateProductAsync(product);
    }
}