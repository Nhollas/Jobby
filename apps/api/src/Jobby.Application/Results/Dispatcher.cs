using FluentValidation;
using MediatR;

namespace Jobby.Application.Results;

public interface IDispatcher
{
    Task<IDispatchResult<TResponse>> Dispatch<TResponse>(IRequest<IDispatchResult<TResponse>> request, CancellationToken cancellationToken = default)
        where TResponse : class;
}

public class Dispatcher(IMediator mediator) : IDispatcher
{
    public async Task<IDispatchResult<TResponse>> Dispatch<TResponse>(IRequest<IDispatchResult<TResponse>> request, CancellationToken cancellationToken = default) where TResponse : class
    {
        try
        {
            return await mediator.Send(request, cancellationToken);
        }
        catch (ValidationException validationException)
        {
            return DispatchResults.UnprocessableEntity<TResponse>(validationException.Errors
                .Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToArray());
        }
        catch (InvalidOperationException invalidOperationException)
        {
            return DispatchResults.BadRequest<TResponse>(invalidOperationException.Message);
        }
    }
}