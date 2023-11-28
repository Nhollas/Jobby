namespace Jobby.Application.Results;

public class DispatchCreatedResult<TResponse> : IDispatchResult<TResponse> where TResponse : class
{
    public TResponse Response { get; }

    public DispatchCreatedResult(TResponse response)
    {
        Response = response ?? throw new ArgumentNullException(nameof(response));
    }
}