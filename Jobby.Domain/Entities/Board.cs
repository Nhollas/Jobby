using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class Board : Entity
{
    private List<JobList> _jobLists = new();
    private readonly List<Activity> _activities = new();
    private readonly List<Job> _jobs = new();
    private readonly List<Contact> _contacts = new();

    public Board()
    {

    }

    private Board(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string name,
        List<JobList> jobLists)
        : base(id, createdDate, ownerId)
    {
        _jobLists = jobLists;
        Name = name;
    }

    public string Name { get; private set; }

    public IReadOnlyCollection<JobList> JobList => _jobLists;

    public IReadOnlyCollection<Activity> Activities => _activities;

    public IReadOnlyCollection<Job> Jobs => _jobs;

    public IReadOnlyCollection<Contact> Contacts => _contacts;

    public static Board Create(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string name,
        List<JobList> jobLists)
    {
        var board = new Board(
            id,
            createdDate,
            ownerId,
            name,
            jobLists);

        return board;
    }

    public void SetBoardName(string name)
    {
        Name = name;
    }

    public void ChangeJobListPosition(Guid jobListId, int targetIndex)
    {
        JobList jobListToMove = _jobLists.FirstOrDefault(i => i.Id == jobListId);

        int startIndex = _jobLists.IndexOf(jobListToMove);

        _jobLists[startIndex].Index = targetIndex;
        _jobLists[targetIndex].Index = startIndex;
    }
}
