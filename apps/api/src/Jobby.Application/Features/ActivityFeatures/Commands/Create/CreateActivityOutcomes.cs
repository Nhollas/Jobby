namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

public enum CreateActivityOutcomes
{
    ActivityCreated,
    UnknownBoard,
    JobDoesNotExistInBoard,
    UnauthorizedBoardAccess,
    ValidationFailure,
    UnknownError
}