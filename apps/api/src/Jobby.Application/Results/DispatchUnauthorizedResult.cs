namespace Jobby.Application.Results;

public interface IDispatchUnauthorizedResult<out TResponse> : IDispatchResult<TResponse>
{
    string ErrorMessage { get; }
}

public class DispatchUnauthorizedResult<TResponse> : IDispatchUnauthorizedResult<TResponse>
{
    public string ErrorMessage { get; }
    
    public DispatchUnauthorizedResult(string errorMessage)
    {
        ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
    }
}