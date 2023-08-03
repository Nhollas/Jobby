namespace Jobby.Application.Abstractions.Behaviours;

public interface IRequiresAuthorization
{
    public string UserId { get; set; }
}