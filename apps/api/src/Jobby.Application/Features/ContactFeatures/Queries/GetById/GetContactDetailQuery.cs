using Jobby.Application.Contracts.Contact;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
public sealed record GetContactDetailQuery(Guid ContactId) : IRequest<BaseResult<GetContactDetailResponse, GetContactDetailOutcomes>>;