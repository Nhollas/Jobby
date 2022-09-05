using Jobby.Client.Contracts.Auth;
using Jobby.Client.Interfaces;
using Jobby.Client.Services.Base;

namespace Jobby.Client.Services;

public class AuthFeaturesService : BaseDataService, IAuthFeaturesService
{
    public AuthFeaturesService(IClient client) : base(client)
    {
    }

    public async Task<AuthenticateResponse> Authenticate(LoginRequest model)
    {
        AuthenticateRequest request = new()
        {
            Username = model.Username,
            Password = model.Password
        };

        return await _client.LoginAsync(request);
    }

    public async Task<RegisterResponse> Register(Contracts.Auth.RegisterRequest model)
    {
        Base.RegisterRequest request = new()
        {
            Username = model.Username,
            Password = model.Password,
            Email = model.Email
        };

        return await _client.RegisterAsync(request);
    }
}
