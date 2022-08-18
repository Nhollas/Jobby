namespace Jobby.Core.Contracts.Auth;

public class AuthenticateResponse
{
    public string Token { get; private set; }
    public string Id { get; private set; }
    public string Username { get; private set; }

    public AuthenticateResponse(string token, string id, string username)
    {
        Token = token;
        Id = id;
        Username = username;
    }
}
