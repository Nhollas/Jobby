using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Delete;

public sealed record DeleteActivityCommand(string ActivityReference) : IRequest<BaseResult<DeleteActivityResponse, DeleteActivityOutcomes>>;
