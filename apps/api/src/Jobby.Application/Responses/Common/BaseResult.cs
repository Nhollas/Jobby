using System.Text.Json;
using FluentValidation.Results;
using OpenTelemetry.Trace;

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
        
        string dtoTypeName = typeof(TR).Name;
        string outComeTypeName = typeof(TO).Name;
        
        using var span = TracerProvider.Default.GetTracer("Jobby.HttpApi").StartActiveSpan($"{dtoTypeName}-{outComeTypeName}");
        
        span.SetAttribute("IsSuccess", IsSuccess);
        span.SetAttribute("Outcome", Outcome.ToString());
        span.SetAttribute("ErrorMessage", ErrorMessage);

        if (ValidationResult is not null)
        {
            span.SetAttribute("ValidationResult", JsonSerializer.Serialize(ValidationResult));
        }
        
        if (Response is not null)
        {
            span.SetAttribute("Response", JsonSerializer.Serialize(Response));
        }
        
    }
    
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
    public TR Response { get; set; }
    public ValidationResult ValidationResult { get; set; }
    public TO Outcome { get; set; }
}