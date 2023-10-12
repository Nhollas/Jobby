using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.ListFeatures.Commands.Delete;
public sealed record DeleteListCommand(string ListReference) : IRequest<BaseResult<DeleteListResponse, DeleteListOutcomes>>;