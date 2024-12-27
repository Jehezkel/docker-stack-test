using Backend.Api.Endpoint;
using Backend.Api.Security;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Users;

public static class ActivateUser
{
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("users/{userId:Guid}:activate", async ([FromRoute] Guid userId, IUserService userService) =>
            {
                
                await userService.ActivateUser(userId);
                return Results.Ok();
            });
        }
    }
}