using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using Jobby.Domain.Dtos.Contact;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

public sealed record CreateContactCommand : IRequest<BaseResult<ContactDto, CreateContactOutcomes>>
{
    public string BoardReference { get; set; } = null;
    public List<string> JobReferences { get; set; } = new();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public SocialDto Socials { get; set; }
    public List<CreateEmailDto> Emails { get; set; }
    public List<CreatePhoneDto> Phones { get; set; }
    public List<string> Companies { get; set; }
}