using Jobby.Core.Exceptions.Common;
using System.Net;

namespace Jobby.Core.Exceptions.Auth;

public class InvalidUserOrPassException : Exception, IServiceException
{
    public InvalidUserOrPassException()
    {
        const string message = "You have entered an invalid Username or Password";
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage { get; }
}
