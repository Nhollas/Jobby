namespace Jobby.Application.Results;

public class DispatchNotFoundResult<TResponse> : IDispatchResult<TResponse> where TResponse : class
{
    public string ErrorMessage { get; }

    public DispatchNotFoundResult(string? errorMessage)
    {
        ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
    }
}