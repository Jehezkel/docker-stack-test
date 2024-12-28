using Backend.Api.DAL;
using Backend.Api.Endpoint;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Products;

public static class GetProductById
{
    private record Response(Guid Id, string Name, string EAN);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("products/{productId:Guid}", async ([FromRoute] Guid productId, AppDbContext appDbContext) =>
            {
                var product = await appDbContext.Products.FindAsync(productId);
                if (product == null) return Results.NotFound();
                var result = new Response(product.Id, product.Name, product.EAN);
                return Results.Ok(result);
            }).RequireAuthorization();
        }
    }
}