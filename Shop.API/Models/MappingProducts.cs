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
                .ForMember(c => c.CategoryName, m => m.MapFrom(s => s.Category!.Name))
                .ForMember(p => p.ProductPicture, m => m.MapFrom<ProductUrlResolver>())
                .ReverseMap();

            CreateMap<CreateProductDto, EProduct>().ReverseMap();
            CreateMap<EProduct, UpdateProductDto>().ReverseMap();
        }
    }
}
