namespace Jobby.Application.Contracts.Auth;

public class RegisterRequest
{
    public string Username { get; }
    public string Password { get; }
    public string Email { get; }

    public RegisterRequest(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
}
