using Jobby.Client.Contracts.Auth;
using Jobby.Client.Services.Base;

namespace Jobby.Client.Interfaces;

public interface IAuthFeaturesService
{
    Task<AuthenticateResponse> Authenticate(LoginRequest model);
    Task<RegisterResponse> Register(Contracts.Auth.RegisterRequest model);
}
