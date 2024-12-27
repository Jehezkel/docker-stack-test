namespace Backend.Api.Security;

public interface IAuth0Client
{
    public Task UpdateUserId(string subjectId, Guid appUserId);
}