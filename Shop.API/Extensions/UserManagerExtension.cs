using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using System.Security.Claims;

namespace Shop.API.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?> FindUserByClaimPrincipamWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);

        }

        public static async Task<AppUser?> FindEmailByClaimPrincipal(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await userManager.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
