using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update;

public record UpdateActivityCommand(
    string ActivityReference,
    string Title,
    int Type,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    string Note,
    bool Completed,
    string JobReference = "") : IRequest<IDispatchResult<ActivityDto>>;