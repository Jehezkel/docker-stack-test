using System.Reflection;
using Backend.Api.Handlers;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

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



app.MapOpenApi();
app.Run();
