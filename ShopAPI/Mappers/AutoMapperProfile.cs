using AutoMapper;
using ShopAPI.DTO.Order;
using ShopAPI.DTO.Product;
using ShopAPI.Models;

namespace ShopAPI.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddOrderDto, Order>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());
        
        CreateMap<Order, GetOrderDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        
        CreateMap<AddProductDto, Product>();
        
        CreateMap<Product, GetProductDto>();
    }
}