using AutoMapper;
using Shop.Core.Dto;
using Shop.Core.Entities;

namespace Shop.API.MyHelper
{
    public class ProductUrlResolver(IConfiguration configuration) : IValueResolver<EProduct, ProductDto, string>
    {
        private readonly IConfiguration Configuration = configuration;

        public string Resolve(EProduct source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Picture))
            {
                return Configuration["API_url"] + source.Picture;
            }
            return null;
        }
    }
}
