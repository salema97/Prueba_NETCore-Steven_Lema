using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Shop.Core.Entities;
using Shop.Core.Interface;
using Shop.Core.Services;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Data.Config;
using Shop.Infrastructure.Repository;
using Strathweb.AspNetCore.AzureBlobFileProvider;
using System.Text;

namespace Shop.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenServices, TokenService>();
            services.AddScoped<IOrderService, OrderService>();

            try
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    string? connectionString = configuration.GetConnectionString("MySqlConnection");
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                });

                services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

                services.AddMemoryCache();

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]!)),
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false,
                    };
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error durante la configuración del DbContext: {ex.Message}");
            }
            return services;
        }

        public static async void InfrastructureConfigMiddleware(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                await IdentitySeed.SeedUserAsync(userManager);
            }
        }

        public static void ConfigureFilesStatic(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration Configuration)
        {
            var staticFilesPath = Configuration.GetSection("StaticFilesPath").Value;

            Console.WriteLine(staticFilesPath);

            if (env.IsDevelopment())
            {
                var staticFilesFolder = Configuration.GetSection("StaticFilesFolder").Value;
                if (!string.IsNullOrWhiteSpace(staticFilesFolder))
                {
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(
                            Directory.GetCurrentDirectory()
                            )),
                        RequestPath = staticFilesPath
                    });
                }
            }
            else
            {
                var blobFileProvider = app.ApplicationServices.GetRequiredService<AzureBlobFileProvider>();
                if (blobFileProvider != null)
                {
                    app.UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = blobFileProvider,
                        RequestPath = staticFilesPath
                    });
                }
            }
        }
    }
}
