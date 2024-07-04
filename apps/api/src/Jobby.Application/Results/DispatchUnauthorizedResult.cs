namespace Jobby.Application.Results;

public interface IDispatchUnauthorizedResult<out TResponse> : IDispatchResult<TResponse>
{
    string ErrorMessage { get; }
}

public class DispatchUnauthorizedResult<TResponse>(string resource) : IDispatchUnauthorizedResult<TResponse>
{
    public string ErrorMessage { get; } = $"You are not authorised to access the resource {resource}.";
}