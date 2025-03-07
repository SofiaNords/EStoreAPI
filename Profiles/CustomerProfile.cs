using AutoMapper;

namespace EStoreAPI.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Entities.Customer, Models.CustomerDto>();

            CreateMap<Models.CustomerForCreationDto, Entities.Customer>();

            CreateMap<Models.CustomerForUpdateDto, Entities.Customer>();
        }
    }
}
