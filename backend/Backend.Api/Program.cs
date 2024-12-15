using System.Reflection;
using Backend.Api.Handlers;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
builder.Services.AddOpenApi();
builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var origins = config.GetValue<string>("CORS:allowedOrigins") ?? throw new ArgumentNullException("CORS:allowedOrigins");
builder.Services.AddCors(conf => conf.AddDefaultPolicy(c => c.WithOrigins(origins)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseHttpsRedirection();
app.MapGet("/weather-forecast", async (IMediator mediator) =>
{
   return await mediator.Send(new WeatherForecastQuery());
});
app.UseCors();



app.MapOpenApi();
app.Run();
