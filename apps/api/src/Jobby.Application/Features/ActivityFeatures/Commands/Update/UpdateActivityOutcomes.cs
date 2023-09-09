namespace Jobby.Application.Features.ActivityFeatures.Commands.Update;

public enum UpdateActivityOutcomes
{
    JobDoesNotBelongToBoard,
    ActivityUpdated,
    ValidationFailure,
    UnauthorizedJobAccess,
    UnknownJob,
    UnauthorizedActivityAccess,
    UnknownActivity,
    UnknownError
}