using Jobby.Application.Dtos;
using Jobby.Application.Results;
using Jobby.Domain.Dtos.Contact;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

public record CreateContactCommand(
    string BoardReference,
    List<string> JobReferences,
    string FirstName,
    string LastName,
    string JobTitle,
    string Location,
    SocialDto Socials,
    List<CreateEmailDto> Emails,
    List<CreatePhoneDto> Phones,
    List<string> Companies) : IRequest<IDispatchResult<ContactDto>>;