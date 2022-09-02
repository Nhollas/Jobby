using Jobby.Application.Contracts.Contact;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetList;

public sealed record GetBoardContactListQuery(Guid BoardId) : IRequest<List<ListContactsResponse>>;
