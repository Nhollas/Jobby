namespace Jobby.Application.Features.JobFeatures.Queries.GetById;

public enum GetJobDetailOutcomes
{
    UnknownError,
    UnauthorizedJobAccess,
    UnknownJob,
    JobFound
}