using Jobby.Domain.Primitives;

namespace Jobby.Application.Services;

public class ResourceResult<TEntity> : Entity
    where TEntity : Entity
{
    public ResourceResult(
        bool isSuccess,
        Outcome outcome,
        string errorMessage = null,
        TEntity response = null)
    {
        IsSuccess = isSuccess;
        Outcome = outcome;
        ErrorMessage = errorMessage;
        Response = response;
    }
    
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
    public TEntity Response { get; set; }
    public Outcome Outcome { get; set; }
}

public enum Outcome
{
    NotFound,
    Unauthorised,
    Success
}