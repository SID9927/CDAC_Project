using AutoMapper;
using DotnetBackend.DTO;
using DotnetBackend.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();
        // Add additional mappings if needed

        CreateMap<CategoryStockDetail, CategoryStockDetailDTO>();
        CreateMap<CategoryStockDetailDTO, CategoryStockDetail>();

        CreateMap<Farmer, FarmerDTO>();
        CreateMap<FarmerDTO, Farmer>();


        CreateMap<StockDetail, StockDetailDTO>();
        CreateMap<StockDetailDTO, StockDetail>();

        CreateMap<OrderDetail, OrderDetailDTO>();
        CreateMap<OrderDetailDTO, OrderDetail>();

        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();

        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
}
