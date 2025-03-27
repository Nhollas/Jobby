using System.Net;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.HttpApi.Tests.Setup;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

public class DeleteActivity_WithValidDetails_Fixture(JobbyHttpApiFactory factory) : IAsyncLifetime
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

[Collection("SqlCollection")]
public class DeleteActivity_WithValidDetails(
    JobbyHttpApiFactory factory,
    DeleteActivity_WithValidDetails_Fixture fixture)
    : IClassFixture<DeleteActivity_WithValidDetails_Fixture>
{
    private HttpResponseMessage Response => fixture.Response;
    private string ActivityReference => fixture.ActivityReference;
    
    [Fact]
    public void ThenReturns200Ok()
    {
        Response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ThenRemovesActivityInDatabase()
    {
        await using JobbyDbContext context = factory.GetDbContext();

        Activity? deletedActivity = await context.Activities
            .FirstOrDefaultAsync(act => act.Reference == ActivityReference);

        deletedActivity.Should().BeNull();
    }
}