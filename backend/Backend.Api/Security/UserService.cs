using Backend.Api.DAL;
using Backend.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Security;

public class UserService : IUserService
{
    private readonly AppDbContext _appDbContext;
    private readonly IAuth0Client _auth0Client;
    private readonly IUserIdCacheStore _userIdCacheStore;

    public UserService(AppDbContext appDbContext, IAuth0Client auth0Client, IUserIdCacheStore userIdCacheStore)
    {
        _appDbContext = appDbContext;
        _auth0Client = auth0Client;
        _userIdCacheStore = userIdCacheStore;
    }

    public async Task<Guid> EnsureCreated(string auth0Sub)
    {
        var existingUser = await _appDbContext.Users.SingleOrDefaultAsync(u => u.Subject == auth0Sub);

        if (existingUser is not null) return existingUser.AppUserId;

        var newUser = new AppUserEntity { Subject = auth0Sub, IsActive = false };
        await _appDbContext.Users.AddAsync(newUser);
        await _appDbContext.SaveChangesAsync();
        return newUser.AppUserId;
    }

    public async Task<Guid> GetUserAppId(string auth0Sub)
    {
        var userIdFromCache = await _userIdCacheStore.GetUserId(auth0Sub);
        if (userIdFromCache is not null)
        {
            return userIdFromCache.Value;
        }
        var userIdFromDb = await EnsureCreated(auth0Sub);
        await _userIdCacheStore.SetUserId(auth0Sub, userIdFromDb);
        
        return userIdFromDb;
    }

    public async Task ActivateUser(Guid userId)
    {
        var user = await _appDbContext.Users.FindAsync(userId);

        if (user is null)
        {
            throw new InvalidOperationException($"User with id {userId} has not been found.");
        }

        user.IsActive = true;
        await _appDbContext.SaveChangesAsync();
        if (user.Subject is null) throw new InvalidOperationException($"User with id {userId} has not been activated.");
        await _auth0Client.UpdateUserId(user.Subject, userId);
    }
}