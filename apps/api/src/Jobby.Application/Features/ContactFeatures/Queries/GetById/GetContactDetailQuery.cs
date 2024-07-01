using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
public record GetContactDetailQuery(string ContactReference) : IRequest<IDispatchResult<ContactDto>>;