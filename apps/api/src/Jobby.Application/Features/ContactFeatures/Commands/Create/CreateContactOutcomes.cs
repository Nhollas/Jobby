namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

public enum CreateContactOutcomes
{
    UnauthorizedBoardAccess,
    UnknownBoard,
    UnknownError,
    ContactCreated,
    ValidationFailure
}