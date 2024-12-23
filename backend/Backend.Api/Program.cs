using System.Reflection;
using Backend.Api.Handlers;
using Backend.Api.Security;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
services.AddOpenApi();
services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var origins = config.GetValue<string>("CORS:allowedOrigins") ?? throw new ArgumentNullException("CORS:allowedOrigins");
services.AddCors(conf => conf.AddDefaultPolicy(c => c.WithOrigins(origins).AllowAnyHeader()));

services.AddJwt(config);

var app = builder.Build();

app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/weather-forecast", async (IMediator mediator) =>
{
   return await mediator.Send(new WeatherForecastQuery());
}).RequireAuthorization();



app.MapOpenApi();
app.Run();
