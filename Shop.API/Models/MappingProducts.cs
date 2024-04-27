using AutoMapper;
using Shop.Core.Dto;
using Shop.Core.Entities;
namespace Product.API.Models
{
    public class MappingProducts : Profile
    {
        public MappingProducts()
        {
            CreateMap<EProduct, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category!.Name))
                .ForMember(d => d.ProductPicture, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();

            CreateMap<CreateProductDto, EProduct>().ReverseMap();
            CreateMap<EProduct, UpdateProductDto>().ReverseMap();
        }
    }
}
