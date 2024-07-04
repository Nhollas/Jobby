namespace Jobby.Application.Results;

public interface IDispatchOkResult<out TResponse> : IDispatchResult<TResponse>
{
    TResponse Response { get; }
}

public class DispatchOkResult<TResponse>(TResponse response) : IDispatchOkResult<TResponse>
{
    public TResponse Response { get; } = response ?? throw new ArgumentNullException(nameof(response));
}