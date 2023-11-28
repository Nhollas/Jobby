namespace Jobby.Application.Results;

public class DispatchBadRequestResult<TResponse> : DispatchResult<TResponse> where TResponse : class
{
    public ValidationError[] ValidationErrors { get; }

    public DispatchBadRequestResult(ValidationError[] validationErrors)
    {
        ValidationErrors = validationErrors ?? throw new ArgumentNullException(nameof(validationErrors));
    }
}