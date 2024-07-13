using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete.Fixtures;

public class DeleteActivityWithValidDetailsFixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    public string ActivityReference { get; private set; } = string.Empty;
    public Board PreLoadedBoard { get; private set; } = null!;
    
    public async Task InitializeAsync()
    {
        (Board preLoadedBoard, Activity activityToDelete) = await SeedDataHelper.CreateBoardWithActivityAsync(factory, "TestUserId");
        PreLoadedBoard = preLoadedBoard;
        
        ActivityReference = activityToDelete.Reference;
        Response = await factory.HttpClient.DeleteAsync($"/activity/{activityToDelete.Reference}");
    }

    public Task DisposeAsync() => Task.CompletedTask;
}