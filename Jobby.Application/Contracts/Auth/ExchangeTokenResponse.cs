namespace Jobby.Application.Contracts.Auth;

public class ExchangeTokenResponse
{
    public string BearerToken { get; set; }
    public long? ExpiresAt { get; set; } = null;
}