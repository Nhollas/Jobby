using Jobby.Client.Interfaces;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels;

namespace Jobby.Client.Services;

public class AuthFeaturesService : BaseDataService, IAuthFeaturesService
{
    public AuthFeaturesService(IClient client) : base(client)
    {
    }

    public async Task<AuthenticateResponse> Authenticate(LoginViewModel model)
    {
        AuthenticateRequest request = new()
        {
            Username = model.Username,
            Password = model.Password
        };

        return await _client.LoginAsync(request);
    }

    public async Task<RegisterResponse> Register(RegisterViewModel model)
    {
        RegisterRequest request = new()
        {
            Username = model.Username,
            Password = model.Password,
            Email = model.Email
        };

        return await _client.RegisterAsync(request);
    }
}
