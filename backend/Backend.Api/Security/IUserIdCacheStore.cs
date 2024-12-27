namespace Backend.Api.Security;

public interface IUserIdCacheStore
{
    public Task<Guid?> GetUserId(string subjectId);
    public Task SetUserId(string subjectId, Guid userId);
}