namespace Jobby.Application.Results;

public interface IDispatchBadRequestResult<out TResponse> : IDispatchResult<TResponse>
{
    string ErrorMessage { get; }
}

public class DispatchBadRequestResult<TResponse>(string errorMessage) : IDispatchBadRequestResult<TResponse>
{
    public string ErrorMessage { get; } = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
}