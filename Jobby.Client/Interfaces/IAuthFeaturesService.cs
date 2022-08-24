using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.AuthViewModels;

namespace Jobby.Client.Interfaces;

public interface IAuthFeaturesService
{
    Task<AuthenticateResponse> Authenticate(LoginViewModel model);
    Task<RegisterResponse> Register(RegisterViewModel model);
}
