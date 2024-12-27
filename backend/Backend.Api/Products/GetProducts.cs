using Backend.Api.DAL;
using Backend.Api.DAL.Entities;
using Backend.Api.Endpoint;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Products;

public static class GetProducts
{
   public record Request();
   public record Response(ProductEntry[] Items, int Total, int Page, int Limit);
   public record ProductEntry(Guid ProductId, string EAN, string Name);

   public class Endpoint : IEndpoint
   {
      public void MapEndpoint(IEndpointRouteBuilder app)
      {
         app.MapGet("products", Handler);
      }

      public async Task<IResult> Handler(AppDbContext appDbContext, [FromQuery] int page = 1, [FromQuery] int limit = 20)
      {

         if (page < 1 || limit < 1)
         {
            return TypedResults.BadRequest("PageNumber and PageSize must be greater than one");
         }

         var skip = (page - 1) * limit;
         var totalCount = await appDbContext.Products.CountAsync();

         if (totalCount < skip)
         {
            return TypedResults.BadRequest("Page out of collection range");
         }

         var productEntities = await appDbContext.Products
            .OrderBy(p => p.Id)
            .Skip(skip)
            .Take(limit)
            .ToListAsync();


         var result = new Response
         (
            Items: [.. productEntities.Select(ToEntry)],
            Page: page,
            Limit: limit,
            Total: totalCount);

         return TypedResults.Ok(result);
      }
   }

   private static ProductEntry ToEntry(ProductEntity entity) => new ProductEntry(entity.Id, entity.EAN, entity.Name);
}

