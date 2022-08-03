using Jobby.Core.Contracts.Auth;

namespace Jobby.Core.Interfaces;

public interface IAuthenticationService
{
    public Task<AuthenticateResponse> Login(AuthenticateRequest request);
    public Task<RegisterResponse> Register(RegisterRequest request);
}
