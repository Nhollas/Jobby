using System.Text.Json.Serialization;

namespace Jobby.Application.Contracts.Auth;

public class GoogleRefreshResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }
    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }
    [JsonPropertyName("scope")]
    public string Scope { get; set; }
}