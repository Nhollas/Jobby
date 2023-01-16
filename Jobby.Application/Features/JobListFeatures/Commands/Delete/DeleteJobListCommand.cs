using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Delete;
public sealed record DeleteJobListCommand(Guid Id) : IRequest;