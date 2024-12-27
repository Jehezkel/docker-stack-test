namespace Backend.Api.Security;

public record Auth0ClientSettings
{
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Url { get; set; } = default!;
}