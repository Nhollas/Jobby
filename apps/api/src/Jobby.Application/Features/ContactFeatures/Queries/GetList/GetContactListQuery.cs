using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetList;

public record GetContactListQuery : IRequest<IDispatchResult<List<ContactDto>>>;