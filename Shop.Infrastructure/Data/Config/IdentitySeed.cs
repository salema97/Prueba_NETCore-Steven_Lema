using Microsoft.AspNetCore.Identity;
using Shop.Core.Entities;

namespace Shop.Infrastructure.Data.Config
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Developer",
                    Email = "salemavelasquez97@gmail.com",
                    UserName = "salemavelasquez97@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Steven",
                        LastName = "Lema",
                        City = "Latacunga",
                        State = "Cotopaxi",
                        Street = "Tanicuchi",
                        PostalCode = "050112"
                    }
                };

                await userManager.CreateAsync(user, "P@$$w0rd");
            }
        }
    }
}
