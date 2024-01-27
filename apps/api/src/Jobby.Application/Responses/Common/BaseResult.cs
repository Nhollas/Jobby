using FluentValidation.Results;

namespace Jobby.Application.Responses.Common;

public record BaseResult<TR, TO> where TR : class where TO : Enum
{
    public BaseResult(bool IsSuccess, TO Outcome, string ErrorMessage = null, TR Response = null, ValidationResult ValidationResult = null)
    {
        this.IsSuccess = IsSuccess;
        this.Outcome = Outcome;
        this.ErrorMessage = ErrorMessage;
        this.Response = Response;
        this.ValidationResult = ValidationResult;
    }
    
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
    public TR Response { get; set; }
    public ValidationResult ValidationResult { get; set; }
    public TO Outcome { get; set; }
}