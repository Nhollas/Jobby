using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete.Fixtures;

public class DeleteActivityWithValidDetailsFixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    
    public string ActivityReference { get; private set; } = string.Empty;
    private HttpClient HttpClient => factory.SetupClient();
    
    private readonly Board _preLoadedBoard = Board.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "TestBoard");

    
    public async Task InitializeAsync()
    {
        await using JobbyDbContext context = factory.GetDbContext();
        
        await SeedDataHelper<Board>.AddAsync(_preLoadedBoard, context);
        
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
        
        Activity preLoadedActivity = await SeedDataHelper<Activity>.AddAsync(activityToDelete, context);
        
        Response = await HttpClient.DeleteAsync($"/activity/{preLoadedActivity.Reference}");
    }

    public async Task DisposeAsync()
    {
        await using JobbyDbContext context = factory.GetDbContext();
        
        Board? preLoadedBoard = await context.Boards.FirstOrDefaultAsync(b => b.Reference == _preLoadedBoard.Reference);

        if (preLoadedBoard is null) return;
        
        await SeedDataHelper<Board>.RemoveAsync(preLoadedBoard, context);
    }
}