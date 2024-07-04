namespace Jobby.Application.Results;

public interface IDispatchCreatedResult<out TResponse> : IDispatchResult<TResponse>
{
    TResponse Response { get; }
}

public class DispatchCreatedResult<TResponse>(TResponse response) : IDispatchCreatedResult<TResponse>
{
    public TResponse Response { get; } = response ?? throw new ArgumentNullException(nameof(response));
}