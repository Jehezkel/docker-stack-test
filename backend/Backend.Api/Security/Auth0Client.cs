using Microsoft.Extensions.Options;

namespace Backend.Api.Security;

public class Auth0Client : IAuth0Client
{
    private readonly IOptions<Auth0ClientSettings> _options;
    private readonly HttpClient _httpClient;
    private readonly ILogger<Auth0Client> _logger;

    public Auth0Client(IOptions<Auth0ClientSettings> options, HttpClient httpClient, ILogger<Auth0Client> logger)
    {
        _options = options;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task UpdateUserId(string subjectId, Guid appUserId)
    {
        var reqUrl = "/oauth/token";
        var message = new HttpRequestMessage(HttpMethod.Post, reqUrl);
        message.Content = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", _options.Value.ClientId),
            new KeyValuePair<string, string>("client_secret", _options.Value.ClientSecret),
            new KeyValuePair<string, string>("audience", _options.Value.Audience)
        ]);
        var response = await _httpClient.SendAsync(message);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Unable to get access token from auth0");
        }

        var content = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
        var updateUserUrl = $"/api/v2/users/{subjectId}";
        var updateMessage = new HttpRequestMessage(HttpMethod.Patch, updateUserUrl);
        updateMessage.Headers.Add("Authorization", $"Bearer {content.AccessToken}");
        var messageContent = new { app_metadata = new { app_user_id = appUserId.ToString() } };
        updateMessage.Content = JsonContent.Create(messageContent);
        var updateResponse = await _httpClient.SendAsync(updateMessage);
        var updateResponseContent = await updateResponse.Content.ReadAsStringAsync();
        _logger.LogInformation(updateResponseContent);
    }
}