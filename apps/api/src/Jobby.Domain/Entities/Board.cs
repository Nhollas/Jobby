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
    public IReadOnlyCollection<Job> Jobs { get; init; }
    public IReadOnlyCollection<Contact> Contacts { get; init; }

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
        
        List<JobList> defaultJobLists =
        [
            JobList.Create(createdDate, ownerId, "Applied", 0, board),
            JobList.Create(createdDate, ownerId, "Wishlist", 1, board),
            JobList.Create(createdDate, ownerId, "Interview", 2, board),
            JobList.Create(createdDate, ownerId, "Offer", 3, board),
            JobList.Create(createdDate, ownerId, "Rejected", 4, board)
        ];
        
        board.SetJobLists(defaultJobLists);

        return board;
    }

    public void SetBoardName(string name)
    {
        Name = name;
    }

    public bool BoardOwnsList(string jobListReference)
    {
        return _lists
            .Select(list => list.Reference == jobListReference)
            .Any();
    }

    public void SetJobLists(List<JobList> jobLists)
    {
        _lists.AddRange(jobLists);
    }
    
    public Activity CreateActivity(
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
