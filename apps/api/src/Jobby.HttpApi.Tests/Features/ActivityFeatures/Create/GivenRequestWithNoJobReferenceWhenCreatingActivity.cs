using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.HttpApi.Tests.Setup;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

public class GivenRequestWithNoJobReferenceWhenCreatingActivityFixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    public ActivityDto ReturnedActivity { get; private set; } = null!;
    public Activity StoredActivity { get; private set; } = null!;
    public CreateActivityCommand Body { get; private set; } = null!;
    
    public async Task InitializeAsync()
    {
        await using JobbyDbContext dbContext = factory.GetDbContext();
        
        Board board = await new TestDataBuilder(factory)
            .CreateBoard()
            .SeedAsync();

        Body = new CreateActivityCommand(
            BoardReference: board.Reference,
            Title: "Test Activity",
            Type: ActivityConstants.Types.Apply,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(1),
            Note: "Test Note",
            Completed: false
        );
        
        Response = await factory.HttpClient.PostAsJsonAsync("/activity", Body);
        ReturnedActivity = (await Response.Content.ReadFromJsonAsync<ActivityDto>())!;
        StoredActivity = await dbContext.Activities
            .Include(activity => activity.Job)
            .SingleAsync(activity => activity.Reference == ReturnedActivity.Reference);
    }

    public Task DisposeAsync() => Task.CompletedTask;
}

[Collection("SqlCollection")]
public class GivenRequestWithNoJobReferenceWhenCreatingActivity(GivenRequestWithNoJobReferenceWhenCreatingActivityFixture fixture) : IClassFixture<GivenRequestWithNoJobReferenceWhenCreatingActivityFixture>
{
    private const string ExpectedActivityName = "Apply";
    
    [Fact]
    public void ThenReturns201Created() => 
        fixture.Response.StatusCode.Should().Be(HttpStatusCode.Created);
    
    [Fact]
    public void ThenReturnedActivityHasCorrectDetails()
    {
        using AssertionScope _ = new();
        
        fixture.ReturnedActivity.Title.Should().Be(fixture.Body.Title);
        fixture.ReturnedActivity.Name.Should().Be(ExpectedActivityName);
        fixture.ReturnedActivity.StartDate.Should().Be(fixture.Body.StartDate);
        fixture.ReturnedActivity.EndDate.Should().Be(fixture.Body.EndDate);
        fixture.ReturnedActivity.Note.Should().Be(fixture.Body.Note);
        fixture.ReturnedActivity.Completed.Should().Be(fixture.Body.Completed);
        fixture.ReturnedActivity.BoardReference.Should().Be(fixture.Body.BoardReference);
        fixture.ReturnedActivity.Type.Should().Be((int)fixture.Body.Type);
    }
    
    [Fact]
    public void ThenReturnedActivityDoesNotHaveJobLinked() => 
        fixture.ReturnedActivity.Job.Should().BeNull();

    [Fact]
    public void ThenStoredActivityHasCorrectDetails()
    {
        using AssertionScope _ = new();
        
        fixture.StoredActivity.Title.Should().Be(fixture.Body.Title);
        fixture.StoredActivity.Name.Should().Be(ExpectedActivityName);
        fixture.StoredActivity.StartDate.Should().Be(fixture.Body.StartDate);
        fixture.StoredActivity.EndDate.Should().Be(fixture.Body.EndDate);
        fixture.StoredActivity.Note.Should().Be(fixture.Body.Note);
        fixture.StoredActivity.Completed.Should().Be(fixture.Body.Completed);
        fixture.StoredActivity.BoardReference.Should().Be(fixture.Body.BoardReference);
        fixture.StoredActivity.Type.Should().Be((int)fixture.Body.Type);
    }
    
    [Fact]
    public void ThenStoredActivityDoesNotHaveJobLinked()
    {
        fixture.StoredActivity.Job.Should().BeNull();
    }
}