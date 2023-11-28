using FluentValidation;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Behaviours;

public class ValidatingMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class, IDispatchResult<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;


    public ValidatingMiddleware(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }


    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);


            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));


            ValidationError[] validationErrors = validationResults
                .SelectMany(x => x.Errors)
                .Select(x => new ValidationError(x.PropertyName, x.ErrorMessage))
                .ToArray();


            if (validationErrors.Length != 0)
            {
                return null;
            }
        }


        return await next();
    }
}