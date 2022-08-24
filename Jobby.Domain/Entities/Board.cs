using Jobby.Domain.Entities.Common;

namespace Jobby.Domain.Entities;

public class Board : BaseEntity
{
    private readonly List<JobList> _jobLists = new();
    private readonly List<Activity> _activities = new();
    private readonly List<Contact> _contacts = new();

    private Board()
    {

    }

    private Board(
        DateTime createdDate,
        string ownerId,
        string name,
        List<JobList> jobLists)
        : base(createdDate, ownerId)
    {
        _jobLists = jobLists;
        Name = name;
    }

    public string Name { get; private set; }

    public IReadOnlyCollection<JobList> JobList => _jobLists;

    public IReadOnlyCollection<Activity> Activities => _activities;

    public IReadOnlyCollection<Contact> Contacts => _contacts;


    public static Board Create(
        DateTime createdDate,
        string ownerId, 
        string name,
        List<JobList> jobLists)
    {
        var board = new Board(
            createdDate,
            ownerId,
            name,
            jobLists);

        return board;
    }

    public void AddActivity(Activity activity)
    {
        _activities.Add(activity);
    }

    public void AddContact(Contact contact)
    {
        _contacts.Add(contact);
    }

    public void SetBoardName(string name)
    {
        Name = name;
    }
}
