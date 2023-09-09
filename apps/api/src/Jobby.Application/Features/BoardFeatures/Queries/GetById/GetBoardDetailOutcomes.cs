namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

public enum GetBoardDetailOutcomes
{
    UnauthorizedBoardAccess,
    UnknownBoard,
    UnknownError,
    BoardFound
}