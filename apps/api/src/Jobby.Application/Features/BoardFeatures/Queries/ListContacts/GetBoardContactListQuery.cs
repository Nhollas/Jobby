using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListContacts;

public sealed record GetBoardContactListQuery(string BoardReference) : IRequest<List<ContactDto>>;
