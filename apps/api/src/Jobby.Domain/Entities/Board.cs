using Jobby.Domain.Dtos.Contact;
using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class Board : Entity
{
    private readonly List<JobList> _lists = [];
    private readonly List<Activity> _activities = [];
    private readonly List<Contact> _contacts = [];
    private Board(){}

    private Board(
        string reference,
        DateTimeOffset createdDate,
        string ownerId,
        string name)
        : base(reference, createdDate, ownerId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; private set; } = string.Empty;
    public IReadOnlyCollection<JobList> Lists => _lists;
    public IReadOnlyCollection<Activity> Activities => _activities;
    public IReadOnlyCollection<Job> Jobs { get; private set; } = new List<Job>();
    public IReadOnlyCollection<Contact> Contacts => _contacts;

    public static Board Create(
        TimeProvider timeProvider,
        string ownerId,
        string name)
    {
        Board board = new(
            reference: EntityReferenceProvider<Board>.CreateReference(),
            createdDate: timeProvider.GetUtcNow(),
            ownerId,
            name);
        
        string[] defaultJobListNames = ["Applied", "Wishlist", "Interview", "Offer", "Rejected"];

        foreach (string listName in defaultJobListNames)
        {   
            board.AddJobList(timeProvider, listName);
        }

        return board;
    }
    
    public JobList AddJobList(
        TimeProvider timeProvider,
        string name)
    {
        JobList createdJobList = JobList.Create(
            createdDate: timeProvider.GetUtcNow(),
            OwnerId,
            name,
            position: _lists.Count + 1,
            this);
        
        _lists.Add(createdJobList);
        
        return createdJobList;
    }
    
    public Activity AddActivity(
        TimeProvider timeProvider,
        string title, 
        int type, 
        DateTimeOffset startDate, 
        DateTimeOffset endDate, 
        string note, 
        bool completed, 
        string? jobReference = null)
    {
        Activity activity = Activity.Create(
            createdDate: timeProvider.GetUtcNow(),
            OwnerId,
            title,
            type,
            startDate,
            endDate,
            note,
            completed,
            this);
        
        _activities.Add(activity);

        if (string.IsNullOrEmpty(jobReference)) return activity;

        if (!TryGetJobFromBoard(jobReference, out Job? jobToLink))
        {
            throw new InvalidOperationException(
                $"The Job {jobReference} you wanted to link doesn't exist in the Board {Reference}."
            );
        }
            
        activity.SetJob(jobToLink!);

        return activity;
    }
    
    public Contact AddContact(
        TimeProvider timeProvider,
        string firstName,
        string lastName,
        string jobTitle,
        string location,
        Social socials,
        IEnumerable<string> companies,
        IEnumerable<CreateEmailDto> emails,
        IEnumerable<CreatePhoneDto> phones)
    {
        Contact contact = Contact.Create(
            createdDate: timeProvider.GetUtcNow(),
            OwnerId,
            firstName,
            lastName,
            jobTitle,
            location,
            socials,
            this,
            companies,
            emails,
            phones);

        _contacts.Add(contact);

        return contact;
    }

    public void UpdateBoardName(string name) => Name = name;

    public bool DoesBoardOwnList(string jobListReference) => 
        _lists
        .Select(list => list.Reference == jobListReference)
        .Any();

    private bool TryGetJobFromBoard(string jobReference, out Job? job)
    {
        job = _lists
            .SelectMany(list => list.Jobs
            .Where(job => job.Reference == jobReference))
            .FirstOrDefault();

        return job != null;
    }
}
