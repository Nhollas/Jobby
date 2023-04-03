using System.Text.Json.Serialization;

namespace Jobby.Application.Contracts.Auth;

public class GithubRefreshResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    [JsonPropertyName("refresh_token_expires_in")]
    public long RefreshTokenExpiresIn { get; set; }
    [JsonPropertyName("scope")]
    public string Scope { get; set; }
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
}