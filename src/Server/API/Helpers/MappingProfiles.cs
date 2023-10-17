using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductReturnDto>()
                .ForMember(dest => dest.ProductType,
                    opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.ProductBrand,
                    opt => opt.MapFrom(src => src.ProductBrand.Name));
        }
    }
}
