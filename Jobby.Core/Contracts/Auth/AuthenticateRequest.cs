namespace Jobby.Application.Contracts.Auth;

public class AuthenticateRequest
{
    public string Username { get; }
    public string Password { get; }

    public AuthenticateRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
