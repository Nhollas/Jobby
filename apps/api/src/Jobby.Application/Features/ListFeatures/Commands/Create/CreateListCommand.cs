using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.ListFeatures.Commands.Create;
public record CreateListCommand(
    string BoardReference,
    string Name,
    int Index,
    string JobReference = "")
    : IRequest<IDispatchResult<JobListDto>>;
