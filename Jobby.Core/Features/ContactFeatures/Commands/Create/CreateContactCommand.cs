using MediatR;

namespace Jobby.Core.Features.ContactFeatures.Commands.Create;

public class CreateContactCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public Guid BoardId { get; set; }
    public string TwitterUri { get; set; }
    public string FacebookUri { get; set; }
    public string LinkedInUri { get; set; }
    public string GithubUri { get; set; }
    public Guid[] JobIds { get; set; }
    public string[] Emails { get; set; }
    public string[] Phones { get; set; }
    public string[] Companies { get; set; }
}
