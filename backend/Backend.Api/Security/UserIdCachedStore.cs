using Microsoft.Extensions.Caching.Memory;

namespace Backend.Api.Security;

public class UserIdCachedStore(IMemoryCache memoryCache) : IUserIdCacheStore
{
    public Task<Guid?> GetUserId(string subjectId)
    {
        var userId = memoryCache.Get<Guid?>(GenerateCacheKey(subjectId));
        return Task.FromResult(userId);
    }

    public Task SetUserId(string subjectId, Guid userId)
    {
        memoryCache.Set(GenerateCacheKey(subjectId), userId);
        return Task.CompletedTask;
    }
    private string GenerateCacheKey(string auth0Subject) => $"SubKey:{auth0Subject}";
}