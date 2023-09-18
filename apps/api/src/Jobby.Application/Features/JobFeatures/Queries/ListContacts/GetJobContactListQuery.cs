using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListContacts;

public sealed record GetJobContactListQuery(Guid JobId) : IRequest<List<ContactDto>>;
