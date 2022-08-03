namespace Jobby.Core.Exceptions.Auth;

public class InvalidUserOrPassException : BadRequestException
{
    public InvalidUserOrPassException()
        : base("You have entered an invalid Username or Password")
    {
    }
}
