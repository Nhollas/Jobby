using Jobby.Application.Contracts.Auth;

namespace Jobby.Application.Interfaces.Services;

public interface IAuthenticationService
{
    public Task<AuthenticateResponse> Login(AuthenticateRequest request);
    public Task<RegisterResponse> Register(RegisterRequest request);
}
