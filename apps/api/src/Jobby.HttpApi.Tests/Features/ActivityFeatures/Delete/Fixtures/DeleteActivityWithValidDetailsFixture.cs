using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.HttpApi.Tests.Setup;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete.Fixtures;

public class DeleteActivityWithValidDetailsFixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    public string ActivityReference { get; private set; } = string.Empty;
    public Board PreLoadedBoard { get; private set; } = null!;
    
    public async Task InitializeAsync()
    {
        Board board = await new TestDataBuilder(factory)
            .CreateBoard()
            .WithActivity()
            .SeedAsync();
        PreLoadedBoard = board;
        
        ActivityReference = board.Activities.First().Reference;
        Response = await factory.HttpClient.DeleteAsync($"/activity/{ActivityReference}");
    }

    public Task DisposeAsync() => Task.CompletedTask;
}