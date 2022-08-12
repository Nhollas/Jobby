using Jobby.Core.Exceptions.Common;
using System.Net;

namespace Jobby.Core.Exceptions;
public class NotFoundException : Exception, IServiceException
{
    private string _message;

    public NotFoundException(string message)
    {
        _message = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => _message;
}
