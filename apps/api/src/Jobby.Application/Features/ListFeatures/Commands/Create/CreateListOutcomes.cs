namespace Jobby.Application.Features.ListFeatures.Commands.Create;

public enum CreateListOutcomes
{
    UnknownError,
    UnauthorizedJobAccess,
    UnknownJob,
    ListCreated,
    UnauthorizedBoardAccess,
    UnknownBoard
}