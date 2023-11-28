using FluentValidation.Results;

namespace Jobby.Application.Results;

public interface IDispatchResult<TResponse> where TResponse : class {}

public abstract class DispatchResult<TResponse>: IDispatchResult<TResponse> where TResponse : class {}

public static class DispatchResults
{
    public static DispatchOkResult<TResponse> Ok<TResponse>(TResponse response) where TResponse : class
        => new(response);
    
    public static DispatchCreatedResult<TResponse> Created<TResponse>(TResponse response) where TResponse : class
        => new(response);
    
    public static DispatchUnauthorizedResult<TResponse> Unauthorized<TResponse>(string errorMessage) where TResponse : class
        => new(errorMessage);
    
    public static DispatchNotFoundResult<TResponse> NotFound<TResponse>(string errorMessage) where TResponse : class
        => new(errorMessage);
    
    public static DispatchBadRequestResult<TResponse> BadRequest<TResponse>(ValidationError[] validationErrors) where TResponse : class
        => new(validationErrors);
    
    public static DispatchUnprocessableEntityResult<TResponse> UnprocessableEntity<TResponse>(ValidationResult validationResult) where TResponse : class
        => new(validationResult);
    
}