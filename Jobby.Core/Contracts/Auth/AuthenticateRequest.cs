namespace Jobby.Core.Contracts.Auth;

public class AuthenticateRequest
{
    public string Username { get; private set; }
    public string Password { get; private set; }

    public AuthenticateRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
