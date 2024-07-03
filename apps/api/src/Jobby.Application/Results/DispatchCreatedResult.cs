namespace Jobby.Application.Results;

public interface IDispatchCreatedResult<out TResponse> : IDispatchResult<TResponse>
{
    TResponse Response { get; }
}

public class DispatchCreatedResult<TResponse> : IDispatchCreatedResult<TResponse>
{
    public TResponse Response { get; }

    public DispatchCreatedResult(TResponse response)
    {
        Response = response ?? throw new ArgumentNullException(nameof(response));
    }
}