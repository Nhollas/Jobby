﻿using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

public sealed record CreateContactCommand : IRequest<ContactDto>
{
    public Guid? BoardId { get; set; }
    public List<Guid> JobIds { get; set; } = new();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public SocialDto Socials { get; set; }
    public List<EmailRequest> Emails { get; set; }
    public List<PhoneRequest> Phones { get; set; }
    public List<string> Companies { get; set; }
}

public record EmailRequest
{
    public string Name { get; set; }
    public DataType Type { get; set; }
}

public record PhoneRequest
{
    public string Number { get; set; }
    public DataType Type { get; set; }
}

public enum DataType
{
    Work,
    Personal,
}