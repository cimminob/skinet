using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{

    //MappingProfile is the configuration for the AutoMapper extension used with Dto
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                /* configure mapping of productBrand. ProductBrand is the destination
                    d - the destination
                    o - the options
                    s - the source, where property comes from to be inserted
                */
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                // configure mapping of product Type
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                //configure mapping of image url using ProductUrlResolver class
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

        }
    }
}