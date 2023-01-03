namespace Jobby.Application.Contracts.Auth;

public class AuthenticateResponse
{
    public string Name { get; }
    public string Email { get; set; }
    public string AccessToken { get; set; }

    public AuthenticateResponse(string name, string email, string accessToken)
    {
        Name = name;
        Email = email;
        AccessToken = accessToken;
    }
}
