namespace Jobby.Application.Exceptions.Base;
public class NotAuthorisedException : UnauthorizedAccessException
{
    public NotAuthorisedException(string userId)
        : base($"User {userId} does not have access to this resource.")
    {
    }
}
