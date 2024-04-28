using Microsoft.OpenApi.Models;
using Shop.API.Extensions;
using Shop.API.Middleware;
using Shop.Infrastructure;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddApiRegistration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Auth Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {securitySchema, new[] { "Bearer"} }
    };

    swagger.AddSecurityDefinition("Bearer", securitySchema);
    swagger.AddSecurityRequirement(securityRequirement);


});

builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.AddSingleton<IConnectionMultiplexer>(i =>
{
    var configure = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis")!, true);
    return ConnectionMultiplexer.Connect(configure);
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
InfrastructureRegistration.InfrastructureConfigMiddleware(app);

app.Run();