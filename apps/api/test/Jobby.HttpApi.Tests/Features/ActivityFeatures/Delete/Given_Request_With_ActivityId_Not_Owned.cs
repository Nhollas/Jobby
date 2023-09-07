using System.Net;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class Given_Request_With_ActivityId_Not_Owned : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_ActivityId_Not_Owned(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    private HttpClient HttpClient => _factory.SetupClient();
    

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Then_Returns_403_Forbidden()
    {
        await using var context = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        var preLoadedBoard = await SeedDataHelper<Board>.AddAsync(Board.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserTwoId", "TestBoard", new List<JobList>()), context);
        
        var activityToDelete = Activity.Create(
            id: Guid.NewGuid(),
            createdDate: DateTime.UtcNow,
            ownerId: "TestUserTwoId",
            title: "Test Activity",
            activityType: 1,
            startDate: DateTime.UtcNow,
            endDate: DateTime.UtcNow,
            note: "Test Note",
            completed: false,
            board: preLoadedBoard
        );
        
        var preLoadedActivity = await SeedDataHelper<Activity>.AddAsync(activityToDelete, context);
        
        var response = await HttpClient.DeleteAsync($"/api/activity/delete/{preLoadedActivity.Id}");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}