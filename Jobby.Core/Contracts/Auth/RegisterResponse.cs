namespace Jobby.Core.Contracts.Auth;

public class RegisterResponse
{
    public string Username { get; private set; }
    public string Email { get; private set; }

    public RegisterResponse(string username, string email)
    {
        Username = username;
        Email = email;
    }
}
