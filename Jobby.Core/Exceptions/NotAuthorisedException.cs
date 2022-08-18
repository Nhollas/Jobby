using Jobby.Core.Exceptions.Common;
using System.Net;

namespace Jobby.Core.Exceptions;
public class NotAuthorisedException : Exception, IServiceException
{
    public NotAuthorisedException(string message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    public string ErrorMessage { get; }
}
