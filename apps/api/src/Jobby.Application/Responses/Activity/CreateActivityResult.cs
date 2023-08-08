using FluentValidation.Results;

namespace Jobby.Application.Responses.Activity;

public record CreateActivityResult(bool IsSuccess, 
    CreateActivityOutcome Outcome,
    string ErrorMessage = null,
    CreateActivityResponse Response = null,
    ValidationResult ValidationResult = null)
{
    public CreateActivityResponse Response { get; set; } = Response;
    public bool IsSuccess { get; set; } = IsSuccess;
    public string ErrorMessage { get; set; } = ErrorMessage;
    public ValidationResult ValidationResult { get; set; } = ValidationResult;
    public CreateActivityOutcome Outcome { get; set; } = Outcome;
}