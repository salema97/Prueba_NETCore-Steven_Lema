using Microsoft.Extensions.FileProviders;
using Shop.API.Middleware;
using Shop.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
    //options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
    );
//builder.Services.AddApiReguestration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InfrastructureConfiguration(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(
    Directory.GetCurrentDirectory(), "wwwroot"
    )));

//builder.Services.AddSingleton<IConnectionMultiplexer>(i =>
//{
//    var configure = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
//    return ConnectionMultiplexer.Connect(configure);
//});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();