using Jobby.Core.Entities.Common;

namespace Jobby.Core.Entities;

public class Board : BaseEntity
{
    private Board()
    {
    }

    public Board(
        string name,
        string ownerId,
        List<JobList> jobsList)
    {
        Name = name;
        OwnerId = ownerId;
        JobList = jobsList;
    }

    public string Name { get; set; }
    public ICollection<JobList> JobList { get; set; }
    public ICollection<Activity> Activities { get; set; }
    public ICollection<Contact> Contacts { get; set; }

    public void AddActivity(Activity activity)
    {
        (Activities ??= new List<Activity>()).Add(activity);
    }

    public void AddContact(Contact contact)
    {
        (Contacts ??= new List<Contact>()).Add(contact);
    }
}
