using Backend.Api.DAL;
using Backend.Api.Endpoint;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Products;

public static class UpdateProduct
{
    public record Request(string Name);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("products/{productId:Guid}",
                async ([FromRoute] Guid productID, [FromBody] Request request, AppDbContext appDbContext) =>
                {
                    var product = await appDbContext.Products.FindAsync(productID);
                    if (product == null) return Results.NotFound();
                    product.Name = request.Name;
                    await appDbContext.SaveChangesAsync();
                    return Results.Accepted();
                });
        }
    }
}