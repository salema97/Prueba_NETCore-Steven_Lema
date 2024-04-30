using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Shop.API.Errors;
using System.Reflection;

namespace Shop.API.Extensions
{
    public static class APIRegistration
    {
        public static IServiceCollection AddApiRegistration(this IServiceCollection services, WebApplicationBuilder builder)
        {
            try
            {
                //public static void ConfigureFilesStatic(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration Configuration)
                //{
                //    var staticFilesPath = Configuration.GetSection("StaticFilesPath").Value;

                //    Console.WriteLine(staticFilesPath);

                //    if (env.IsDevelopment())
                //    {
                //        var staticFilesFolder = Configuration.GetSection("StaticFilesFolder").Value;
                //        if (!string.IsNullOrWhiteSpace(staticFilesFolder))
                //        {
                //            app.UseStaticFiles(new StaticFileOptions
                //            {
                //                FileProvider = new PhysicalFileProvider(Path.Combine(
                //                    Directory.GetCurrentDirectory()
                //                    )),
                //                RequestPath = staticFilesPath
                //            });
                //        }
                //    }
                //    else
                //    {
                //        var blobFileProvider = app.ApplicationServices.GetRequiredService<AzureBlobFileProvider>();
                //        if (blobFileProvider != null)
                //        {
                //            app.UseStaticFiles(new StaticFileOptions()
                //            {
                //                FileProvider = blobFileProvider,
                //                RequestPath = staticFilesPath
                //            });
                //        }
                //    }
                //}

                services.AddAutoMapper(Assembly.GetExecutingAssembly());

                services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(
                    builder.Environment.ContentRootPath, "wwwroot"
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
                        .WithOrigins("https://prueba-shop-viamatica.azurewebsites.net");
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
