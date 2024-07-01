using Jobby.Domain.Entities;
using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Helpers;

public static class SeedBundleHelper
{
    public static async Task<Board> AddBoardWithJobAsync(JobbyDbContext context, string? userId = null)
    {
        Board board = Board.Create(DateTime.UtcNow, userId ?? "TestUserId", "TestBoard");
        
        JobList jobList = JobList.Create(DateTime.UtcNow, userId ?? "TestUserId", "TestJobList", 0);
        
        Job job = Job.Create(DateTime.UtcNow, userId ?? "TestUserId", "TestJob", "TestTitle", 0, jobList, board);
        
        jobList.AddJob(job);

        List<JobList> jobLists = [jobList];
        
        board.SetJobLists(jobLists);
        
        await context.Boards.AddAsync(board);
        
        await context.SaveChangesAsync();

        return board;
    }
}