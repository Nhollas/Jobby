using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Update;

public sealed record UpdateContactCommand : IRequest
{
    public Guid ContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public SocialDto Socials { get; set; }
    public List<Guid> JobIds { get; set; }
    public List<CompanyDto> Companies { get; set; }
    public List<EmailDto> Emails { get; set; }
    public List<PhoneDto> Phones { get; set; }
}
