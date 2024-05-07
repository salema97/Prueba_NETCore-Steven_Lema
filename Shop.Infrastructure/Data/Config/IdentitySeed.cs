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
                var address = new Address
                {
                    FirstName = "Steven",
                    LastName = "Lema",
                    City = "Latacunga",
                    State = "Cotopaxi",
                    Street = "Tanicuchi",
                    PostalCode = "050112"
                };

                var user = new AppUser()
                {
                    DisplayName = "Developer",
                    Email = "salemavelasquez97@gmail.com",
                    UserName = "salemavelasquez97@gmail.com",
                    Address = address
                };

                await userManager.CreateAsync(user, "P@$$w0rd");
            }
        }
    }
}
