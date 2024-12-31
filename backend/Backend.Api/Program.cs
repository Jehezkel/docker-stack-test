using System.Reflection;
using Backend.Api.DAL;
using Backend.Api.Endpoint;
using Backend.Api.Handlers;
using Backend.Api.MasterData;
using Backend.Api.Security;
using MediatR;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddOpenApi();
services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

services.AddOptions<Auth0ClientSettings>()
    .BindConfiguration("Auth0ApiClient")
    .ValidateOnStart();

services.AddCors(config);
services.AddJwt(config);
services.AddHttpContextAccessor();
services.AddDatabase(config);
services.AddEndpoints();

services.AddHttpClient<IAuth0Client, Auth0Client>(opt =>
    opt.BaseAddress = new Uri(config["Auth0ApiClient:Url"] ?? throw new InvalidOperationException()));
services.AddMemoryCache();
services.AddScoped<IUserIdCacheStore, UserIdCachedStore>();

services.AddScoped<IUserService, UserService>();
services.AddScoped<ICurrentUserService, CurrentUserService>();
services.AddScoped<IGenericRepository<CategoryEntity>, GenericRepository<CategoryEntity>>();
services.AddScoped<IGenericRepository<ManufacturerEntity>, GenericRepository<ManufacturerEntity>>();
services.AddScoped<IGenericRepository<ParameterEntity>, GenericRepository<ParameterEntity>>();
// services.AddScoped<LocalUserCreationMiddleware>();*/
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LocalUserCreationMiddleware>();
app.MapGet("/weather-forecast",
    async (IMediator mediator, IHttpContextAccessor contextAccessor) =>
    {
        return await mediator.Send(new WeatherForecastQuery());
    }).RequireAuthorization();

app.MapEndpoints();


app.Run();