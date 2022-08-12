using Jobby.Core.Exceptions.Common;
using System.Net;

namespace Jobby.Core.Exceptions;

public class BadRequestException : Exception, IServiceException
{
    private string _message;

    public BadRequestException(string message)
    {
        _message = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => _message;
}
