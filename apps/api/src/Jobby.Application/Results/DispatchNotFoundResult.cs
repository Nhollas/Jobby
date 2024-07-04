namespace Jobby.Application.Results;

public interface IDispatchNotFoundResult<out TResponse> : IDispatchResult<TResponse>
{
    string ErrorMessage { get; }
}

public class DispatchNotFoundResult<TResponse>(string? errorMessage) : IDispatchNotFoundResult<TResponse>
{
    public string ErrorMessage { get; } = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
}