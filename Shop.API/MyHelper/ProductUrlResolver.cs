using AutoMapper;
using Shop.Core.Dto;

public class ProductUrlResolver(IConfiguration configuration) : IValueResolver<Shop.Core.Entities.Product, ProductDto, string?>
{
    private readonly IConfiguration _configuration = configuration;

    public string? Resolve(Shop.Core.Entities.Product source, ProductDto destination, string? destMember, ResolutionContext context)
    {
        try
        {
            if (!string.IsNullOrEmpty(source.Picture))
            {
                return _configuration["API_url"] + source.Picture;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al resolver la URL del producto: {ex.Message}");
        }
    }
}