using Jobby.Application.Contracts.Contact;
using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListContacts;

public sealed record GetJobContactsQuery(Guid JobId) : IRequest<List<GetContactResponse>>;
