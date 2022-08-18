using Jobby.Core.Exceptions.Common;
using System.Net;

namespace Jobby.Core.Exceptions;

public class BadRequestException : Exception, IServiceException
{
    public BadRequestException(string message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage { get; }
}
