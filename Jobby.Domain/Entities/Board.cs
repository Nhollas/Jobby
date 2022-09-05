﻿using Jobby.Domain.Primitives;

namespace Jobby.Domain.Entities;

public class Board : Entity
{
    private readonly List<JobList> _jobLists = new();
    private readonly List<Activity> _activities = new();
    private readonly List<Contact> _contacts = new();
    private readonly List<Job> _jobs = new();

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

    public IReadOnlyCollection<Contact> Contacts => _contacts;

    public IReadOnlyCollection<Job> Jobs => _jobs;


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
