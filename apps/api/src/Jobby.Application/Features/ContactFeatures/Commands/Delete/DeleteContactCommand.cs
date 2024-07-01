using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Delete;

public record DeleteContactCommand(string ContactReference) : IRequest<IDispatchResult<DeleteContactResponse>>;
