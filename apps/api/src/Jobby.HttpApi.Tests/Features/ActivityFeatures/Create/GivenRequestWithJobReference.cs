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

public class GivenRequestWithJobReferenceFixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    public ActivityDto ReturnedActivity { get; private set; } = null!;
    public CreateActivityCommand Body { get; private set; } = null!;
    public Activity StoredActivity { get; private set; } = null!;
    
    public async Task InitializeAsync()
    {
        await using JobbyDbContext dbContext = factory.GetDbContext();
        Board board = await new TestDataBuilder(factory)
            .CreateBoard()
            .WithJob()
            .BuildAsync();
        
        Job job = board.Jobs.First();
        
        Body = new CreateActivityCommand(
            BoardReference: board.Reference,
            Title: "Test Activity",
            Type: ActivityConstants.Types.Apply,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(1),
            Note: "Test Note",
            Completed: false,
            JobReference: job.Reference
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
public class GivenRequestWithJobReference(GivenRequestWithJobReferenceFixture fixture) : IClassFixture<GivenRequestWithJobReferenceFixture>
{
    private CreateActivityCommand Body => fixture.Body;
    private static string ExpectedName => "Apply";
    
    [Fact]
    public void ThenReturns201Created()
    {
        fixture.Response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public void ThenReturnedActivityHasCorrectDetails()
    {
        using AssertionScope _ = new();
        
        fixture.ReturnedActivity.Title.Should().Be(Body.Title);
        fixture.ReturnedActivity.Name.Should().Be(ExpectedName);
        fixture.ReturnedActivity.StartDate.Should().Be(Body.StartDate);
        fixture.ReturnedActivity.EndDate.Should().Be(Body.EndDate);
        fixture.ReturnedActivity.Note.Should().Be(Body.Note);
        fixture.ReturnedActivity.Completed.Should().Be(Body.Completed);
        fixture.ReturnedActivity.BoardReference.Should().Be(Body.BoardReference);
        fixture.ReturnedActivity.Type.Should().Be((int)Body.Type);
    }
    
    [Fact]
    public void ThenReturnedActivityHasJobLinked()
    {
        fixture.ReturnedActivity.Job.Reference.Should().Be(Body.JobReference);
    }
    
    [Fact]
    public void ThenStoredActivityHasCorrectDetails()
    {
        using AssertionScope _ = new();
        
        fixture.StoredActivity.Title.Should().Be(Body.Title);
        fixture.StoredActivity.Name.Should().Be(ExpectedName);
        fixture.StoredActivity.StartDate.Should().Be(Body.StartDate);
        fixture.StoredActivity.EndDate.Should().Be(Body.EndDate);
        fixture.StoredActivity.Note.Should().Be(Body.Note);
        fixture.StoredActivity.Completed.Should().Be(Body.Completed);
        fixture.StoredActivity.BoardReference.Should().Be(Body.BoardReference);
        fixture.StoredActivity.Type.Should().Be((int)Body.Type);
    }
    
    [Fact]
    public void ThenStoredActivityHasJobLinked()
    {
        fixture.StoredActivity.Job!.Reference.Should().Be(Body.JobReference);
    }
}