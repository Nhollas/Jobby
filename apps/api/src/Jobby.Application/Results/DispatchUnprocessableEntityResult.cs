using FluentValidation.Results;

namespace Jobby.Application.Results;


public interface IDispatchUnprocessableEntityResult<out TResponse> : IDispatchResult<TResponse>
{
    ValidationError[] ValidationErrors { get; }
}

public class DispatchUnprocessableEntityResult<TResponse> : IDispatchUnprocessableEntityResult<TResponse>
{
    public DispatchUnprocessableEntityResult(ValidationResult  validationResult)
    {
        ValidationErrors = validationResult.Errors.Select(error =>
            new ValidationError(error.PropertyName, error.ErrorMessage)
        ).ToArray();
    }
    
    public DispatchUnprocessableEntityResult(ValidationError[] validationErrors)
    {
        ValidationErrors = validationErrors;
    }
    
    public ValidationError[]? ValidationErrors { get; set; }
}

public class ValidationError
{
    public ValidationError(string propertyName, string message)
    {
        PropertyName = propertyName;
        Message = message;
    }

    public string PropertyName { get; }
    public string Message { get; }
}