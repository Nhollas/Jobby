namespace Jobby.Application.Contracts.Auth;

public class AuthenticateResponse
{
    public string Token { get; }
    public string Id { get; }
    public string Username { get; }

    public AuthenticateResponse(string token, string id, string username)
    {
        Token = token;
        Id = id;
        Username = username;
    }
}
