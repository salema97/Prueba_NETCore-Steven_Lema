using AutoMapper;
using Shop.Core.Dto;
using Shop.Core.Entities.Orders;

namespace Shop.API.MyHelper
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string member, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrderd?.PictureUrl))
            {
                return _configuration["API_url"] + source.ProductItemOrderd.PictureUrl;
            }
            return null;
        }
    }
}
