using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Shop.API.Errors;
using System.Reflection;

namespace Shop.API.Extensions
{
    public static class APIRegistration
    {
        public static IServiceCollection AddApiRegistration(this IServiceCollection services)
        {
            try
            {
                services.AddAutoMapper(Assembly.GetExecutingAssembly());

                services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(
                    Directory.GetCurrentDirectory(),
                    //LOCAL ROOT
                    //"wwwroot"
                    //HOST ROOT
                    "home\\site\\wwwroot\\wwwroot"
                    )));

                services.Configure
                    <ApiBehaviorOptions>(options =>
                    {
                        options.InvalidModelStateResponseFactory = context =>
                        {
                            var errors = context.ModelState.Values
                                .Where(x => x.Errors.Count > 0)
                                .SelectMany(x => x.Errors)
                                .Select(x => x.ErrorMessage)
                                .ToArray();

                            var errorResponse = new APIValidationErrorResponse(errors);

                            return new BadRequestObjectResult(errorResponse);
                        };
                    });

                services.AddCors(opt =>
                {
                    opt.AddPolicy("CorsPolicy", pol =>
                    {
                        pol.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("*");
                    });
                });

                return services;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error durante el registro de la API : {ex.Message}");
            }
        }
    }
}
