using FluentValidation.Results;

namespace Jobby.Application.Results;

public interface IDispatchResult<out TResponse>;

public static class DispatchResults
{
    public static DispatchOkResult<TResponse> Ok<TResponse>(TResponse response) where TResponse : class => new(response);
    
    public static DispatchCreatedResult<TResponse> Created<TResponse>(TResponse response) where TResponse : class =>
        new(response);
    
    public static DispatchUnauthorizedResult<TResponse> Unauthorized<TResponse>(string resource)
        where TResponse : class =>
        new(resource);
    
    public static DispatchNotFoundResult<TResponse> NotFound<TResponse>(string errorMessage) where TResponse : class =>
        new(errorMessage);
    
    public static DispatchBadRequestResult<TResponse> BadRequest<TResponse>(string errorMessage)
        where TResponse : class =>
        new(errorMessage);
    
    public static DispatchUnprocessableEntityResult<TResponse> UnprocessableEntity<TResponse>(
        ValidationResult validationResult) where TResponse : class =>
        new(validationResult);
    
    public static DispatchUnprocessableEntityResult<TResponse> UnprocessableEntity<TResponse>(
        ValidationError[] validationErrors) where TResponse : class =>
        new(validationErrors);
}