using Shop.Core.Entities;

namespace Shop.Core.Services
{
    public interface ITokenServices
    {
        string CreateToken(AppUser appUser);
    }
}
