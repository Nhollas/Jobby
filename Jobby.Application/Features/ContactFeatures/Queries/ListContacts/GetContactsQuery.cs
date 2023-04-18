using Jobby.Application.Contracts.Contact;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.ListContacts;

public sealed record GetContactsQuery() : IRequest<List<GetContactResponse>>;