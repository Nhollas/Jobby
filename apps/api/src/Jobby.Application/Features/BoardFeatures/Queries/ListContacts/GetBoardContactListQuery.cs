using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListContacts;

public record GetBoardContactListQuery(string BoardReference) : IRequest<IDispatchResult<List<ContactDto>>>;
