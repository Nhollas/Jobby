using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

public sealed record CreateContactCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public Guid BoardId { get; set; }
    public List<Guid> JobIds { get; set; }
    public SocialDto Socials { get; set; }
    public List<EmailDto> Emails { get; set; }
    public List<PhoneDto> Phones { get; set; }
    public List<CompanyDto> Companies { get; set; }
}
