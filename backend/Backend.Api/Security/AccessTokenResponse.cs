using System.Text.Json.Serialization;

namespace Backend.Api.Security;

public class AccessTokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; } = default!;
    [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; } = default!;
    [JsonPropertyName("scope")] public string Scope { get; set; } = default!;
    [JsonPropertyName("token_type")] public string TokenType { get; set; } = default!;
}