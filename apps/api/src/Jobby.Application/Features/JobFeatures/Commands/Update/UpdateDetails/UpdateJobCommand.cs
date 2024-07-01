using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;

public record UpdateJobCommand(
    string JobReference,
    string Company,
    string Title,
    string PostUrl,
    int Salary,
    string City,
    string Colour,
    string Description,
    DateTimeOffset? Deadline)
    : IRequest<IDispatchResult<JobDto>>;
