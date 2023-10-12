using System.Net;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class Given_Request_With_Valid_Details : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_Valid_Details(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }
    
    private HttpClient HttpClient => _factory.SetupClient();

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Then_Returns_200_OK_And_Activity_Is_Deleted()
    {
        await using var initContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        var preLoadedBoard = await SeedDataHelper<Board>.AddAsync(Board.Create(Guid.NewGuid(), 
        
            DateTime.UtcNow, "TestUserId", "TestBoard", new List<JobList>()), initContext);
        
        var activityToDelete = Activity.Create(
            id: Guid.NewGuid(),
 
            createdDate: DateTime.UtcNow,
            ownerId: "TestUserId",
            title: "Test Activity",
            activityType: 1,
            startDate: DateTime.UtcNow,
            endDate: DateTime.UtcNow,
            note: "Test Note",
            completed: false,
            board: preLoadedBoard
        );
        
        var preLoadedActivity = await SeedDataHelper<Activity>.AddAsync(activityToDelete, initContext);
        
        var response = await HttpClient.DeleteAsync($"/activity/{preLoadedActivity.Reference}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        await using var updatedContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        var activity = await updatedContext.Activities.FirstOrDefaultAsync(x => x.Id == preLoadedActivity.Id);
        
        Assert.Null(activity);
    }
}