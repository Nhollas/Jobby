using Jobby.Application.Contracts.Auth;

namespace Jobby.Application.Interfaces.Services;

public interface IAuthenticationService
{
    public Task<ExchangeTokenResponse> ExchangeGoogleToken(ExchangeTokenRequest request);
    public Task<ExchangeTokenResponse> ExchangeGithubToken(ExchangeTokenRequest request);
}
