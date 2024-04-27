using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Core.Interface;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Repository;

namespace Shop.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            try
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    string? connectionString = configuration.GetConnectionString("MySqlConnection");
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error durante la configuración del DbContext: {ex.Message}");
            }
            return services;
        }
    }
}
