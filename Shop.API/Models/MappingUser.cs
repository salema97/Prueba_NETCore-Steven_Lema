using AutoMapper;
using Shop.Core.Dto;
using Shop.Core.Entities;

namespace Shop.API.Models
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
