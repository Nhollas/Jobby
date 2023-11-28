using FluentValidation.Results;

namespace Jobby.Application.Results;

public class DispatchUnprocessableEntityResult<TResponse> : IDispatchResult<TResponse> where TResponse : class
{
    public DispatchUnprocessableEntityResult(ValidationResult  validationResult)
    {
        ValidationErrors = validationResult.Errors.Select(error =>
            new ValidationError(error.PropertyName, error.ErrorMessage)
        ).ToArray();
    }
    
    public ValidationError[]? ValidationErrors { get; set; }
}

public class ValidationError
{
    public ValidationError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }

    public string PropertyName { get; }
    public string ErrorMessage { get; }
}