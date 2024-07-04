using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class Board : Entity
{
    private readonly List<JobList> _lists = [];
    private readonly List<Activity> _activities = [];

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
    public IReadOnlyCollection<Job> Jobs { get; init; } = new List<Job>();
    public IReadOnlyCollection<Contact> Contacts { get; init; } = new List<Contact>();

    public static Board Create(
        DateTimeOffset createdDate,
        string ownerId,
        string name)
    {
        Board board = new(
            reference: EntityReferenceProvider<Board>.CreateReference(),
            createdDate,
            ownerId,
            name);
        
        string[] defaultJobListNames = ["Applied", "Wishlist", "Interview", "Offer", "Rejected"];

        foreach (string jobListName in defaultJobListNames)
        {   
            board.AddJobList(createdDate, jobListName);
        }

        return board;
    }
    
    public JobList AddJobList(
        DateTimeOffset createdDate,
        string name)
    {
        JobList createdJobList = JobList.Create(
            createdDate,
            OwnerId,
            name,
            position: _lists.Count + 1,
            this);
        
        _lists.Add(createdJobList);
        
        return createdJobList;
    }

    public void SetBoardName(string name)
    {
        Name = name;
    }

    public bool BoardOwnsList(string jobListReference) => 
        _lists
        .Select(list => list.Reference == jobListReference)
        .Any();
    
    public Activity AddActivity(
        DateTimeOffset createdDate,
        string title, 
        int type, 
        DateTimeOffset startDate, 
        DateTimeOffset endDate, 
        string note, 
        bool completed, 
        string? jobReference = null)
    {
        Activity activity = Activity.Create(createdDate, OwnerId, title, type, startDate, endDate, note, completed, this);

        if (string.IsNullOrEmpty(jobReference)) return activity;
        if (!TryGetJobFromBoard(jobReference, out Job? jobToLink))
        {
            throw new InvalidOperationException($"The {nameof(Job)} {jobReference} you wanted to link doesn't exist in the Board {Reference}.");
        }
            
        activity.SetJob(jobToLink!);
        _activities.Add(activity);

        return activity;
    }

    private bool TryGetJobFromBoard(string jobReference, out Job? job)
    {
        job = _lists
            .SelectMany(list => list.Jobs
                .Where(job => job.Reference == jobReference))
            .FirstOrDefault();

        return job != null;
    }
}
