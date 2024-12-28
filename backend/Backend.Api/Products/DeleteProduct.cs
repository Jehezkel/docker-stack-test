using Backend.Api.DAL;
using Backend.Api.Endpoint;

namespace Backend.Api.Products;

public static class DeleteProduct
{
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{productId:Guid}", Handler);
        }

        private async Task<IResult> Handler(Guid productId, AppDbContext appDbContext)
        {
            var product =await appDbContext.Products.FindAsync(productId);
            if(product == null) return Results.NotFound();
            appDbContext.Products.Remove(product);
            await appDbContext.SaveChangesAsync();
            return Results.NoContent();
        }
    }
    
}