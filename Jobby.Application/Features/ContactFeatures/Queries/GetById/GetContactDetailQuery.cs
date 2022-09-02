using Jobby.Application.Contracts.Contact;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
public sealed record GetContactDetailQuery(Guid BoardId, Guid ContactId) : IRequest<GetContactResponse>;