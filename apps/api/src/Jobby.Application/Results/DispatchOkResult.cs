namespace Jobby.Application.Results;

public interface IDispatchOkResult<out TResponse> : IDispatchResult<TResponse>
{
    TResponse Response { get; }
}

public class DispatchOkResult<TResponse> : IDispatchOkResult<TResponse>
{
    public TResponse Response { get; }

    public DispatchOkResult(TResponse response)
    {
        Response = response ?? throw new ArgumentNullException(nameof(response));
    }
}