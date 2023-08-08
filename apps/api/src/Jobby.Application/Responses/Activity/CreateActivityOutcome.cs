namespace Jobby.Application.Responses.Activity;

public enum CreateActivityOutcome
{
    ActivityCreated,
    UnknownBoardId,
    JobDoesNotExistInBoard,
    UnauthorizedBoardAccess,
    ValidationFailure
}