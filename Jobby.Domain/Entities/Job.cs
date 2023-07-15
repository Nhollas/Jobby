﻿using Jobby.Domain.Primitives;

namespace Jobby.Domain.Entities;

public class Job : Entity
{
    public Job()
    {

    }

    private Job(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string company,
        string jobTitle,
        int index,
        JobList jobList,
        Board board
        )
        : base(id, createdDate, ownerId)

    {
        Company = company;
        Title = jobTitle;
        Index = index;
        JobList = jobList;
        Board = board;
    }

    public string Company { get; private set; }
    public string Title { get; private set; }
    public string PostUrl { get; private set; }
    public double Salary { get; private set; }
    public string Location { get; private set; }
    public string Description { get; private set; }
    public DateTime? Deadline { get; private set; }
    public int Index { get; private set; }
    public List<Note> Notes { get; } = new();
    public List<Activity> Activities { get; } = new();
    public List<Contact> Contacts { get; } = new();


    // Database Relationship Properties
    public JobList JobList { get; set; }
    public List<JobContact> JobContacts { get; } = new();
    public Guid JobListId { get; set; }
    public Board Board { get; private set; }
    public Guid BoardId { get; private set; }


    public static Job Create(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string company,
        string jobTitle,
        int index,
        JobList jobList,
        Board board)
    {
        var job = new Job(
            id,
            createdDate,
            ownerId,
            company,
            jobTitle,
            index,
            jobList,
            board);

        return job;
    }

    public void SetJobList(Guid jobListId)
    {
        JobListId = jobListId;
    }

    public void SetIndex(int index)
    {
        Index = index;
    }
}
