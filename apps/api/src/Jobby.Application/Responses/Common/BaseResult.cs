using FluentValidation.Results;

namespace Jobby.Application.Responses.Common;

public record BaseResult<TR, TO>(
    bool IsSuccess,
    TO Outcome,
    string ErrorMessage = null,
    TR Response = null,
    ValidationResult ValidationResult = null
    )
    where TR : class where TO : Enum
{
    public bool IsSuccess { get; set; } = IsSuccess;
    public string ErrorMessage { get; set; } = ErrorMessage;
    public TR Response { get; set; } = Response;
    public ValidationResult ValidationResult { get; set; } = ValidationResult;
    public TO Outcome { get; set; } = Outcome;
}