using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update.LinkJob;
public sealed record LinkJobCommand(Guid ActivityId, Guid JobId) : IRequest;
