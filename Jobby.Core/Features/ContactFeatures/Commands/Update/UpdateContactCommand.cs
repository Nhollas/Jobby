using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Update;

public sealed record UpdateContactCommand : IRequest
{
    public Guid ContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Company { get; set; }
    public Guid BoardId { get; set; }
    public string TwitterUri { get; set; }
    public string FacebookUri { get; set; }
    public string LinkedInUri { get; set; }
    public string GithubUri { get; set; }
    public Guid[] JobIds { get; set; }
    public string[] Emails { get; set; }
    public string[] Phones { get; set; }
}
