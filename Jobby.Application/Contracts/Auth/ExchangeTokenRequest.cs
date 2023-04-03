namespace Jobby.Application.Contracts.Auth;

public class ExchangeTokenRequest
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public long ExpiresAt { get; set; }
}