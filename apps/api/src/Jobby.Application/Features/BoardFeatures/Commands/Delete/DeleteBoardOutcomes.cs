namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

public enum DeleteBoardOutcomes
{
    BoardDeleted,
    UnknownError,
    UnauthorizedBoardAccess,
    UnknownBoard
}