using Jobby.Core.Exceptions.Common;
using System.Net;

namespace Jobby.Core.Exceptions;
public class NotAuthorisedException : Exception, IServiceException
{
    private string _message;

    public NotAuthorisedException(string message)
    {
        _message = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    public string ErrorMessage => _message;
}
