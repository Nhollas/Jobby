using Jobby.Application.Contracts.Contact;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListContacts;

public sealed record GetBoardContactListQuery(Guid BoardId) : IRequest<List<GetContactResponse>>;
