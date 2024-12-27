namespace Backend.Api.Security;

public interface IUserService
{
    Task<Guid> GetUserAppId(string auth0Sub);
    Task ActivateUser(Guid userId);
}