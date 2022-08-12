namespace Jobby.Core.Entities;
public class JobContact
{
    public Guid JobFk { get; set; }
    public Job Job { get; set; }

    public Guid ContactFk { get; set; }
    public Contact Contact { get; set; }

    public JobContact(Contact contact, Job job)
    {
        Job = job;
        Contact = contact;
        JobFk = job.Id;
        ContactFk = contact.Id;
    }

    private JobContact()
    {

    }
}
