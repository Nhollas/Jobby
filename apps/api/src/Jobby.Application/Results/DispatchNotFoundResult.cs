namespace Jobby.Application.Results;

public interface IDispatchNotFoundResult<out TResponse> : IDispatchResult<TResponse>
{
    string ErrorMessage { get; }
}

public class DispatchNotFoundResult<TResponse> : IDispatchNotFoundResult<TResponse>
{
    public string ErrorMessage { get; }

    public DispatchNotFoundResult(string? errorMessage)
    {
        ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
    }
}