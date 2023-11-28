namespace Jobby.Application.Results;

public class DispatchUnauthorizedResult<TResponse> : IDispatchResult<TResponse> where TResponse : class
{
    public string ErrorMessage { get; }
    
    public DispatchUnauthorizedResult(string errorMessage)
    {
        ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
    }
}