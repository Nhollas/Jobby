namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;

public enum GetContactDetailOutcomes
{
    UnknownError,
    UnauthorizedContactAccess,
    UnknownContact,
    ContactFound
}