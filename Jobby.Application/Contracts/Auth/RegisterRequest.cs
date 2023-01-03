namespace Jobby.Application.Contracts.Auth;

public class RegisterRequest
{
    public string Username { get; }
    public string Password { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public RegisterRequest(string username, string password, string email, string firstName, string lastName)
    {
        Username = username;
        Password = password;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}
