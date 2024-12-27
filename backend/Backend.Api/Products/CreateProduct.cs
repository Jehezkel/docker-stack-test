using Backend.Api.DAL;
using Backend.Api.DAL.Entities;
using Backend.Api.Endpoint;
using Backend.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Api.Products;

public static class CreateProduct
{
   public record Request(string Name, string EAN);
   public record Response(Guid ProductId);

   public class Endpoint : IEndpoint
   {
      public void MapEndpoint(IEndpointRouteBuilder app)
      {
         app.MapPost("products", Handler);
      }

      public async Task<Created> Handler(Request request, AppDbContext appDbContext)
      {
         var product = new ProductModel(request.Name, request.EAN);
         var productEntity = new ProductEntity
         {
            EAN = product.EAN,
            Name = product.Name,
            Measurements = new MeasurementsEntity
            {
               WidthInCm = product.Measurements.WidthInCm,
               HeightInCm = product.Measurements.HeightInCm,
               LengthInCm = product.Measurements.LengthInCm,
               WeightInKg = product.Measurements.WeightInKg
            }
         };

         await appDbContext.Products.AddAsync(productEntity);
         await appDbContext.SaveChangesAsync();
         return TypedResults.Created();
      }
   }
}
