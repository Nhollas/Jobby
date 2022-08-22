namespace Jobby.Application.Contracts.Auth;

public class RegisterResponse
{
    public string Username { get; }
    public string Email { get; }

    public RegisterResponse(string username, string email)
    {
        Username = username;
        Email = email;
    }
}
