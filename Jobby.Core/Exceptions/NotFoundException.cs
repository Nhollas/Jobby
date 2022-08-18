using Jobby.Core.Exceptions.Common;
using System.Net;

namespace Jobby.Core.Exceptions;
public class NotFoundException : Exception, IServiceException
{
    public NotFoundException(string message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage { get; }
}
