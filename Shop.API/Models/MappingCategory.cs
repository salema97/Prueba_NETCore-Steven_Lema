using AutoMapper;
using Shop.Core.Dto;
using Shop.Core.Entities;

namespace Shop.API.Models
{
    public class MappingCategory : Profile
    {
        public MappingCategory()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<ListCategoryDto, Category>().ReverseMap();
        }
    }
}
