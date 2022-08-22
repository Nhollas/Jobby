using Jobby.Domain.Entities.Common;

namespace Jobby.Domain.Entities;

public class Board : BaseEntity
{
    private Board()
    {
    }

    public Board(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string name,
        List<JobList> jobsList)
        : base(id, createdDate, ownerId)
    {
        Name = name;
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
