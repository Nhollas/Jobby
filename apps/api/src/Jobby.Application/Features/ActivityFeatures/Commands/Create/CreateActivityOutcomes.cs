namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

public enum CreateActivityOutcomes
{
    ActivityCreated,
    UnknownBoardId,
    JobDoesNotExistInBoard,
    UnauthorizedBoardAccess,
    ValidationFailure
}