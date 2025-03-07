using AutoMapper;

namespace EStoreAPI.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Product, Models.ProductDto>();

            CreateMap<Models.ProductForCreationDto, Entities.Product>();

            CreateMap<Models.ProductForUpdateDto, Entities.Product>();
        }
    }
}
