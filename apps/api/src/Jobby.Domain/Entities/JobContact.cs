namespace Jobby.Domain.Entities;
public class JobContact
{
    private JobContact()
    {
    }

    public JobContact(Contact contact, Job job)
    {
        Job = job;
        Contact = contact;
        JobId = job.Id;
        ContactId = contact.Id;
        JobReference = job.Reference;
        ContactReference = contact.Reference;
    }

    public Guid JobId { get; set; }
    public string JobReference { get; set; }
    public Job Job { get; set; }

    public Guid ContactId { get; set; }
    public string ContactReference { get; set; }
    public Contact Contact { get; set; }

}