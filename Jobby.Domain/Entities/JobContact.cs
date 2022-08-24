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
        JobFk = job.Id;
        ContactFk = contact.Id;
    }

    public Guid JobFk { get; set; }
    public Job Job { get; set; }

    public Guid ContactFk { get; set; }
    public Contact Contact { get; set; }
}
