using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Helpers;

public static class SeedDataHelper
{
    public static async Task AddSeedDataAsync(JobbyHttpApiFactory factory)
    {
        var boardOneId = Guid.Parse("b0630723-78d9-4ba3-a825-88714e15aa2c");
        var userTwoBoardId = Guid.Parse("01685b73-0b18-4ef7-a358-37c13f254a28");
        
        var test = new List<Board>
        {
            Board.Create(
                boardOneId, DateTime.UtcNow, "TestUserId", "BoardOne", new List<JobList>()
                {
                    JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "Applied", 0, boardOneId),
                    JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "Wishlist", 1, boardOneId),
                    JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "Interview", 2, boardOneId),
                    JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "Offer", 3, boardOneId),
                    JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "Rejected", 4, boardOneId),
                }
            ),
            Board.Create(
                userTwoBoardId, DateTime.UtcNow, "Test2UserId", "BoardTwo", new List<JobList>()
                {
                    JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "Test2UserId", "Applied", 0, userTwoBoardId),
                    JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "Test2UserId", "Wishlist", 1, userTwoBoardId),
                    JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "Test2UserId", "Interview", 2, userTwoBoardId),
                    JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "Test2UserId", "Offer", 3, userTwoBoardId),
                    JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "Test2UserId", "Rejected", 4, userTwoBoardId),
                }
            ),
        };

        await using var context = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(factory.DbConnectionString).Options);
        
        await context.Boards.AddRangeAsync(test);
        
        await context.SaveChangesAsync();
    }
}