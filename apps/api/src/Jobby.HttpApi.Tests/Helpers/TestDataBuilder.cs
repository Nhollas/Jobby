using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Setup;
using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Helpers;

public class TestDataBuilder
{
    private readonly JobbyHttpApiFactory _factory;
    private readonly JobbyDbContext _context;
    private Board _board = null!;

    public TestDataBuilder(JobbyHttpApiFactory factory)
    {
        _factory = factory;
        _context = factory.GetDbContext();
    }

    public TestDataBuilder CreateBoard(
        string name = "BoardName", 
        string userId = "TestUserId")
    {
        _board = Board.Create(
            _factory.TimeProvider,
            userId,
            name);
        _context.Boards.Add(_board);
        return this;
    }

    public TestDataBuilder WithActivity(
        string title = "ActivityTitle",
        int type = 1,
        DateTimeOffset startDate = default,
        DateTimeOffset endDate = default,
        string note = "ActivityNote",
        bool completed = false)
    {
        _board.AddActivity(
            _factory.TimeProvider,
            title,
            type,
            startDate,
            endDate,
            note,
            completed);

        return this;
    }

    public TestDataBuilder WithJob(
        string company = "JobCompany",
        string title = "JobTitle")
    {
        JobList jobList = _board.Lists.First();
        jobList.CreateJob(_factory.TimeProvider, company, title);
        
        return this;
    }

    public TestDataBuilder WithContact(
        string name = "ContactName",
        string lastName = "ContactLastName",
        string title = "ContactTitle",
        string location = "ContactLocation")
    {
        _board.AddContact(
            _factory.TimeProvider,
            name,
            lastName,
            title,
            location,
            new Social("Twitter", "Facebook", "LinkedIn", "Github"),
            [],
            [],
            []);
        return this;
    }

    public async Task<Board> SeedAsync()
    {
        await _context.SaveChangesAsync();
        return _board;
    }
}