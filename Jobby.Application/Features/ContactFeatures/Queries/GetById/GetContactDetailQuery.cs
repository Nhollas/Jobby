using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
public sealed record GetContactDetailQuery(Guid BoardId, Guid ContactId) : IRequest<ContactDto>;