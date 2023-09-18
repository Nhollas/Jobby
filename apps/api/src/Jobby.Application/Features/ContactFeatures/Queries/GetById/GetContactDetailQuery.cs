using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
public sealed record GetContactDetailQuery(Guid ContactId) : IRequest<BaseResult<ContactDto, GetContactDetailOutcomes>>;