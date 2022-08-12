using System.Net;

namespace Jobby.Core.Exceptions.Common;
public interface IServiceException
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}
