using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetList;

public sealed record GetContactListQuery : IRequest<List<ContactDto>>;