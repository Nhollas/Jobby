namespace Jobby.Application.Results;

public interface IDispatchBadRequestResult<out TResponse> : IDispatchResult<TResponse>
{
    string ErrorMessage { get; }
}

public class DispatchBadRequestResult<TResponse> : IDispatchBadRequestResult<TResponse>
{
    public string ErrorMessage { get; }

    public DispatchBadRequestResult(string errorMessage)
    {
        ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
    }
}