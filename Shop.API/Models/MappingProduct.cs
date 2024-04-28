using AutoMapper;
using Shop.Core.Dto;
namespace Product.API.Models
{
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            CreateMap<Shop.Core.Entities.Product, ProductDto>()
                .ForMember<string>(c => c.CategoryName, m => m.MapFrom(s => s.Category!.Name))
                .ForMember<string>(p => p.Picture, m => m.MapFrom<ProductUrlResolver>())
                .ReverseMap();

            CreateMap<CreateProductDto, Shop.Core.Entities.Product>().ReverseMap();
            CreateMap<Shop.Core.Entities.Product, UpdateProductDto>().ReverseMap();
        }
    }
}
