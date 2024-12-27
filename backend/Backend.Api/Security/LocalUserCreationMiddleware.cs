using System.Security.Claims;
using Backend.Api.DAL;

namespace Backend.Api.Security;

internal class LocalUserCreationMiddleware(RequestDelegate next, ILogger<LocalUserCreationMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context, IUserService userService, ICurrentUserService currentUserService)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var auth0Sub = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ArgumentNullException.ThrowIfNull(auth0Sub);
            var userAppId = await userService.GetUserAppId(auth0Sub);
            currentUserService.UserId = userAppId;
        }
        await next(context);
    }
}