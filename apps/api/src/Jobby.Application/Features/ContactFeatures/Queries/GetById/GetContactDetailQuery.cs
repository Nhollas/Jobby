using Jobby.Application.Contracts.Contact;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
public sealed record GetContactDetailQuery(Guid ContactId) : IRequest<GetContactResponse>;