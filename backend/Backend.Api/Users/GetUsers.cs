using Backend.Api.DAL;
using Backend.Api.Endpoint;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Users;

public static class GetUsers
{
    public record Response(ResponseEntry[] Items);

    public record ResponseEntry(Guid UserId, string FirstName, string LastName);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/users", async (AppDbContext appDbContext) =>
            {
                var result = await appDbContext.Users.ToListAsync();
                return TypedResults.Ok(result);
            });
        }
    }
}