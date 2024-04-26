using AutoMapper;
using Shop.Core.Dto;
using Shop.Core.Entities;

namespace Shop.API.Models
{
    //Asp.Net Core 8 Web API :https://www.youtube.com/watch?v=UqegTYn2aKE&list=PLazvcyckcBwitbcbYveMdXlw8mqoBDbTT&index=1

    public class MappingCategory : Profile
    {
        public MappingCategory()
        {
            CreateMap<CategoryDto, ECategory>().ReverseMap();
            CreateMap<ListCategoryDto, ECategory>().ReverseMap();
        }
    }
}
