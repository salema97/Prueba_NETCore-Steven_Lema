using Shop.Core.Entities;

namespace Shop.Core.Interface
{
    public interface ITokenServices
    {
        string CreateToken(AppUser appUser);
    }
}
