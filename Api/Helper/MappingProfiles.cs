using Api.Dto;
using API.Helpers;
using AutoMapper;
using Core.Models;

namespace Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Product, ProductResponseDto>()
            //    .ForMember(x => x.ProductBrand, o => o.MapFrom(y => y.ProductBrand.Name))
            //    .ForMember(x => x.ProductType, o => o.MapFrom(y => y.ProductType.Name))
            //    .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>())
            //    .ReverseMap();
        }
    }
}
