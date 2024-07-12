using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Helpers;

public static class SeedDataHelper
{
    public static async Task<(Board, Job)> CreateBoardWithJobAsync(JobbyHttpApiFactory factory, string? userId = null)
    {
        await using JobbyDbContext context = factory.GetDbContext();
        
        Board board = Board.Create(factory.TimeProvider, userId ?? "TestUserId", "BoardName");
        JobList jobList = board.AddJobList(factory.TimeProvider, "JobListName");
        Job job = jobList.CreateJob(factory.TimeProvider, "JobCompany", "JobTitle");
        
        await context.Boards.AddAsync(board);
        await context.SaveChangesAsync();
        
        return (board, job);
    }
    
    public static async Task<(Board, Activity)> CreateBoardWithActivityAsync(JobbyHttpApiFactory factory, string? userId = null)
    {
        await using JobbyDbContext context = factory.GetDbContext();
        
        Board board = Board.Create(factory.TimeProvider, userId ?? "TestUserId", "BoardName");
        Activity activity = board.AddActivity(factory.TimeProvider, "ActivityTitle", 1, DateTime.UtcNow, DateTime.UtcNow, "ActivityNote", false);
        
        await context.Boards.AddAsync(board);
        await context.SaveChangesAsync();
        
        return (board, activity);
    }
    
    public static async Task<(Board, Contact, Job[])> GenerateBoardWithContactAndJobsAsync(JobbyHttpApiFactory factory, string? userId = null)
    {
        await using JobbyDbContext context = factory.GetDbContext();

        Board board = Board.Create(
            factory.TimeProvider,
            ownerId: userId ?? "TestUserId",
            name: "BoardName");
        
        Contact contact = board.AddContact(factory.TimeProvider, "ContactName", "ContactLastName", "ContactTitle", "Location",
            new Social("Twitter", "Facebook", "LinkedIn", "Github"), [], [], []);

        JobList singleJobList = board.Lists.First();
        
        const int jobsToCreate = 3;
        for (int i = 0; i < jobsToCreate; i++)
        {
           singleJobList.CreateJob(factory.TimeProvider, "JobCompany", "JobTitle");
        }
        
        contact.SetJobs([.. singleJobList.Jobs]);
        await context.Boards.AddAsync(board);
        await context.SaveChangesAsync();
        
        return (board, contact, singleJobList.Jobs.ToArray());
    }
}