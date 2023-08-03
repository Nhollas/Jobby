namespace Jobby.Application.Exceptions.Base;
public class NotAuthorisedException : UnauthorizedAccessException
{
    public NotAuthorisedException(string userId)
        : base($"You do not have access to this resource.")
    {
    }
}
