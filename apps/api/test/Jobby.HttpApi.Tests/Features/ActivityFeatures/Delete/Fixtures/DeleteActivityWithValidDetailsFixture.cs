using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete.Fixtures;

public class DeleteActivityWithValidDetailsFixture : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public DeleteActivityWithValidDetailsFixture(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }
    
    public HttpResponseMessage Response { get; private set; } = new();
    
    public string ActivityReference { get; private set; } = string.Empty;
    private HttpClient HttpClient => _factory.SetupClient();
    
    private readonly Board _preLoadedBoard = Board.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "TestBoard");

    
    public async Task InitializeAsync()
    {
        await using JobbyDbContext initContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        await SeedDataHelper<Board>.AddAsync(_preLoadedBoard, initContext);
        
        Activity? activityToDelete = Activity.Create(
            id: Guid.NewGuid(),
            createdDate: DateTime.UtcNow,
            ownerId: "TestUserId",
            title: "Test Activity",
            activityType: 1,
            startDate: DateTime.UtcNow,
            endDate: DateTime.UtcNow,
            note: "Test Note",
            completed: false,
            board: _preLoadedBoard
        );
        
        ActivityReference = activityToDelete.Reference;
        
        Activity preLoadedActivity = await SeedDataHelper<Activity>.AddAsync(activityToDelete, initContext);
        
        Response = await HttpClient.DeleteAsync($"/activity/{preLoadedActivity.Reference}");
    }

    public async Task DisposeAsync()
    {
        await using JobbyDbContext disposeContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        Board? preLoadedBoard = await disposeContext.Boards.FirstOrDefaultAsync(b => b.Reference == _preLoadedBoard.Reference);

        if (preLoadedBoard is null) return;
        
        await SeedDataHelper<Board>.RemoveAsync(preLoadedBoard, disposeContext);
    }
}