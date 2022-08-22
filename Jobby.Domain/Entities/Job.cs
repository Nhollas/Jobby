using Jobby.Domain.Entities.Common;

namespace Jobby.Domain.Entities;

public class Job : BaseEntity
{
    public Job(string company, string jobTitle)
    {
        Company = company;
        Title = jobTitle;
    }

    private Job()
    {
    }

    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string HexColour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Notes { get; set; }
    public ICollection<Activity> Activities { get; set; }

    public ICollection<JobContact> JobContacts { get; set; }

    public JobList JobList { get; set; }
    public Guid JobListFk { get; set; }

    public void AddActivity(Activity activity)
    {
        (Activities ??= new List<Activity>()).Add(activity);
    }

    public void AddContact(JobContact contact)
    {
        (JobContacts ??= new List<JobContact>()).Add(contact);
    }
}
