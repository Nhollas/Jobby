namespace Jobby.Core.Contracts.Auth;

public class AuthenticateResponse
{
    public string Token { get; set; }

    public AuthenticateResponse(string token)
    {
        Token = token;
    }
}
