namespace Jobby.Application.Exceptions.Base;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
}
