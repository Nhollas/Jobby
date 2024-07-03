using Jobby.Domain.Entities;
using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Helpers;

public static class SeedDataHelper
{
    public static async Task<T> AddAsync<T>(T entity, JobbyDbContext context) where T : class
    {
        await context.Set<T>().AddAsync(entity);
        
        await context.SaveChangesAsync();
        
        return entity;
    }

    public static async Task RemoveAsync<T>(T entity, JobbyDbContext disposeContext) where T : class
    {
        disposeContext.Set<T>().Remove(entity);
        
        await disposeContext.SaveChangesAsync();
    }
    
    public static async Task<(Board, Job)> CreateBoardWithJobAsync(JobbyDbContext context, string? userId = null)
    {
        Board board = Board.Create(DateTime.UtcNow, userId ?? "TestUserId", "TestBoard");
        
        JobList jobList = JobList.Create(DateTime.UtcNow, userId ?? "TestUserId", "TestJobList", 0);
        
        Job job = Job.Create(DateTime.UtcNow, userId ?? "TestUserId", "TestJob", "TestTitle", 0, jobList, board);
        
        jobList.AddJob(job);

        List<JobList> jobLists = [jobList];
        
        board.SetJobLists(jobLists);
        
        await context.Boards.AddAsync(board);
    
        await context.SaveChangesAsync();
        
        return (board, job);
    }
}