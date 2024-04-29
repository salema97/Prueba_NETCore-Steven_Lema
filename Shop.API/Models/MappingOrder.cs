using AutoMapper;
using Shop.API.MyHelper;
using Shop.Core.Dto;
using Shop.Core.Entities.Orders;

namespace Shop.API.Models
{
    public class MappingOrder : Profile
    {
        public MappingOrder()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price))
                .ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductItemId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ProductItemName, o => o.MapFrom(s => s.ProductItemOrderd!.ProductItemName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ProductItemOrderd!.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>())
                .ReverseMap();
        }
    }
}
