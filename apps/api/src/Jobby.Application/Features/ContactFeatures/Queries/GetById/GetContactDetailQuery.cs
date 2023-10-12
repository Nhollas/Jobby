using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
public sealed record GetContactDetailQuery(string ContactReference) : IRequest<BaseResult<ContactDto, GetContactDetailOutcomes>>;