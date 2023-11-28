using MediatR;

namespace Jobby.Application.Results;

public interface IDispatcher
{
    Task<DispatchResult<TResponse>> Dispatch<TResponse>(IRequest<DispatchResult<TResponse>> request, CancellationToken cancellationToken = default)
        where TResponse : class;
}

public class Dispatcher : IDispatcher
{
    private readonly IMediator _mediator;

    public Dispatcher(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<DispatchResult<TResponse>> Dispatch<TResponse>(IRequest<DispatchResult<TResponse>> request, CancellationToken cancellationToken = default) where TResponse : class
    {
        return await _mediator.Send(request, cancellationToken);
    }
}