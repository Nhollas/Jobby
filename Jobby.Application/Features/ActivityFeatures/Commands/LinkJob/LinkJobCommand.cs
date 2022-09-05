using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.LinkJob;
public sealed record LinkJobCommand(Guid ActivityId, Guid JobId) : IRequest;
