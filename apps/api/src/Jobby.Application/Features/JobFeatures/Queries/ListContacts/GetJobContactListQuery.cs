using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListContacts;

public record GetJobContactListQuery(string JobReference) : IRequest<IDispatchResult<List<ContactDto>>>;
