using Jobby.Domain.Entities.Common;

namespace Jobby.Domain.Entities;

public class Job : BaseEntity
{
    private readonly List<Activity> _activities = new();
    private readonly List<Contact> _contacts = new();
    private readonly List<Note> _notes = new();
    private readonly List<JobContact> _jobContacts = new();

    private Job()
    {

    }

    private Job(
        DateTime createdDate,
        string ownerId,
        string company,
        string jobTitle)
        : base(createdDate, ownerId)

    {
        Company = company;
        Title = jobTitle;
    }

    public string Company { get; private set; }

    public string Title { get; private set; }

    public string PostUrl { get; private set; }

    public int Salary { get; private set; }

    public string City { get; private set; }

    public string HexColour { get; private set; }

    public string Description { get; private set; }

    public DateTime Deadline { get; private set; }

    public IReadOnlyCollection<Note> Notes => _notes;

    public IReadOnlyCollection<Activity> Activities => _activities;

    public IReadOnlyCollection<Contact> Contacts => _contacts;


    // Database Relationship Properties
    public ICollection<JobContact> JobContacts => _jobContacts;
    public virtual JobList JobList { get; set; }
    public Guid JobListFk { get; set; }

    public static Job Create(
        DateTime createdDate,
        string ownerId,
        string company,
        string jobTitle)
    {
        var job = new Job(
            createdDate,
            ownerId,
            company,
            jobTitle);

        return job;
    }

    public void AddActivity(Activity activity)
    {
        _activities.Add(activity);
    }

    public void AddContact(Contact contact)
    {
        _contacts.Add(contact);
    }

    public void AddNote(Note note)
    {
        _notes.Add(note);
    }
}
