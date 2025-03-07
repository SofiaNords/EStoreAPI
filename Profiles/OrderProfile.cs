using AutoMapper;

namespace EStoreAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Entities.Order, Models.OrderDto>();
            CreateMap<Entities.OrderItem, Models.OrderItemDto>();
            CreateMap<Models.OrderDto, Entities.Order>();
            CreateMap<Models.OrderForCreationDto, Entities.Order>();
            CreateMap<Models.OrderItemForCreationDto, Entities.OrderItem>();
        }
        
    }
}
