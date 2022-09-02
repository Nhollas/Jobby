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
    }

    public Guid JobId { get; set; }
    public Job Job { get; set; }

    public Guid ContactId { get; set; }
    public Contact Contact { get; set; }
}
