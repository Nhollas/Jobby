﻿using Jobby.Application.Contracts.Contact;
using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;

public sealed record UpdateContactCommand : IRequest<GetContactResponse>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public SocialDto Socials { get; set; }
    public List<Guid> JobIds { get; set; } = new();
    public Guid? BoardId { get; set; }
    public List<CompanyDto> Companies { get; set; } = new();
    public List<EmailDto> Emails { get; set; } = new();
    public List<PhoneDto> Phones { get; set; } = new();
}
