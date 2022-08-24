using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Delete;

public sealed record DeleteActivityCommand(Guid ActivityId) : IRequest;
