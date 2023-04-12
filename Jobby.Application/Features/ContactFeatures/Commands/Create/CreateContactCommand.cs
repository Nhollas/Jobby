using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

public sealed record CreateContactCommand : IRequest<ContactDto>
{
    public Guid BoardId { get; set; }
    public List<Guid> JobIds { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public SocialDto Socials { get; set; }
    public List<string> Emails { get; set; }
    public List<PhoneDto> Phones { get; set; }
    public List<string> Companies { get; set; }
}
