using Jobby.Core.Entities.Common;

namespace Jobby.Core.Entities;

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
    public string OwnerId { get; set; }
    public ICollection<Activity> Activities { get; set; }

    public ICollection<JobContact> JobContacts { get; set; }


    public JobList JobList { get; set; }
    public Guid JobListFk { get; set; }

    public void AddActivity(Activity activity)
    {
        if (Activities == null)
        {
            Activities = new List<Activity>();
        }

        Activities.Add(activity);
    }

    public void AddContact(JobContact contact)
    {
        if (JobContacts == null)
        {
            JobContacts = new List<JobContact>();
        }

        JobContacts.Add(contact);
    }
}
