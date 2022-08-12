namespace Jobby.Core.Features.ContactFeatures.Queries.GetById;
public class GetContactDetailQuery
{
    public Guid ContactId { get; set; }

    public GetContactDetailQuery(Guid contactId)
    {
        ContactId = contactId;
    }
}
