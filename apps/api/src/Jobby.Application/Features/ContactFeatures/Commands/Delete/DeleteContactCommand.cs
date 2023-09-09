using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Delete;

public sealed record DeleteContactCommand(Guid ContactId) : IRequest<BaseResult<DeleteContactResponse, DeleteContactOutcomes>>;
