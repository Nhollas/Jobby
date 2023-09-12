namespace Jobby.Application.Features.JobFeatures.Commands.Create;

public enum CreateJobOutcomes
{
    UnknownError,
    UnauthorizedBoardAccess,
    UnknownBoard,
    JobCreated,
    JobListNotFound
}