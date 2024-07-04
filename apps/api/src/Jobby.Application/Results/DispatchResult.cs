using FluentValidation.Results;

namespace Jobby.Application.Results;

public interface IDispatchResult<out TResponse>;

public static class DispatchResults
{
    public static DispatchOkResult<TResponse> Ok<TResponse>(TResponse response) where TResponse : class
        => new DispatchOkResult<TResponse>(response);
    
    public static DispatchCreatedResult<TResponse> Created<TResponse>(TResponse response) where TResponse : class
        => new DispatchCreatedResult<TResponse>(response);
    
    public static DispatchUnauthorizedResult<TResponse> Unauthorized<TResponse>(string resource) where TResponse : class
        => new DispatchUnauthorizedResult<TResponse>(resource);
    
    public static DispatchNotFoundResult<TResponse> NotFound<TResponse>(string errorMessage) where TResponse : class
        => new DispatchNotFoundResult<TResponse>(errorMessage);
    
    public static DispatchBadRequestResult<TResponse> BadRequest<TResponse>(string errorMessage) where TResponse : class
        => new DispatchBadRequestResult<TResponse>(errorMessage);
    
    public static DispatchUnprocessableEntityResult<TResponse> UnprocessableEntity<TResponse>(ValidationResult validationResult) where TResponse : class
        => new DispatchUnprocessableEntityResult<TResponse>(validationResult);
    
    public static DispatchUnprocessableEntityResult<TResponse> UnprocessableEntity<TResponse>(ValidationError[] validationErrors) where TResponse : class
        => new DispatchUnprocessableEntityResult<TResponse>(validationErrors);
}