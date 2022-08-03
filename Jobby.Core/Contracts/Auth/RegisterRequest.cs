namespace Jobby.Core.Contracts.Auth;

public class RegisterRequest
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }

    public RegisterRequest(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
}
