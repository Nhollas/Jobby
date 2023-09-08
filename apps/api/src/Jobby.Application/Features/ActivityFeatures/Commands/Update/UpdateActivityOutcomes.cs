namespace Jobby.Application.Features.ActivityFeatures.Commands.Update;

public enum UpdateActivityOutcomes
{
    JobDoesNotBelongToBoard,
    JobDoesNotExist,
    ActivityUpdated,
    ValidationFailure,
    UnauthorizedJobAccess
}