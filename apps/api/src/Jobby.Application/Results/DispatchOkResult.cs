namespace Jobby.Application.Results;

public class DispatchOkResult<TResponse> : IDispatchResult<TResponse> where TResponse : class
{
    public TResponse Response { get; }

    public DispatchOkResult(TResponse response)
    {
        Response = response ?? throw new ArgumentNullException(nameof(response));
    }
}