using Jobby.Application.Dtos;
using Jobby.Application.Results;
using Jobby.Domain.Static;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

public record CreateActivityCommand(
    string BoardReference,
    string Title,
    ActivityConstants.Types Type,
    DateTime StartDate,
    DateTime EndDate,
    string Note,
    bool Completed,
    string? JobReference = null)
    : IRequest<IDispatchResult<ActivityDto>>;
