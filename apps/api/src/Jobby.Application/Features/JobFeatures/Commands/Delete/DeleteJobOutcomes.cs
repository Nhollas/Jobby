namespace Jobby.Application.Features.JobFeatures.Commands.Delete;

public enum DeleteJobOutcomes
{
    UnknownError,
    UnauthorizedJobAccess,
    UnknownJob,
    JobDeleted
}