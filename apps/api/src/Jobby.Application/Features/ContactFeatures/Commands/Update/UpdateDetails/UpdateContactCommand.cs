using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;

public record UpdateContactCommand : IRequest<IDispatchResult<ContactDto>>
{
    public string ContactReference { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public SocialDto Socials { get; set; }
    public List<string> JobReferences { get; set; } = new();
    public string BoardReference { get; set; } = string.Empty;
    public List<CompanyDto> Companies { get; set; } = new();
    public List<EmailDto> Emails { get; set; } = new();
    public List<PhoneDto> Phones { get; set; } = new();
}
