using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.HttpApi.Tests.Setup;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Update;

public class UpdateActivity_WithValidDetails_Fixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    public ActivityDto? ReturnedActivity { get; private set; }
    public UpdateActivityCommand Body { get; private set; } = null!;
    public Activity PreloadedActivity { get; private set; } = null!;


    public async Task InitializeAsync()
    {
        Board board = await new TestDataBuilder(factory)
            .CreateBoard()
            .WithActivity()
            .SeedAsync();
        
        PreloadedActivity = board.Activities.First();

        Body = new UpdateActivityCommand(
            ActivityReference: PreloadedActivity.Reference,
            Title: "Test Activity",
            Type: (int)ActivityConstants.Types.Apply,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(1),
            Note: "Test Note",
            Completed: false);
        
        Response = await factory.HttpClient.PutAsJsonAsync("/activity", Body);
        ReturnedActivity = await Response.Content.ReadFromJsonAsync<ActivityDto>();
    }

    public Task DisposeAsync() => Task.CompletedTask;
}

[Collection("SqlCollection")]
public class UpdateActivity_WithValidDetails(
    UpdateActivity_WithValidDetails_Fixture fixture,
    JobbyHttpApiFactory factory)
    : IClassFixture<UpdateActivity_WithValidDetails_Fixture>
{
    private HttpResponseMessage Response => fixture.Response;
    private UpdateActivityCommand Body => fixture.Body;
    private Activity PreLoadedActivity => fixture.PreloadedActivity;
    private ActivityDto ReturnedActivity => fixture.ReturnedActivity!;
    
    [Fact]
    public void ThenReturns200Ok() => 
        Response.StatusCode.Should().Be(HttpStatusCode.OK);

    [Fact]
    public void ThenReturnsUpdatedActivity()
    {
        using (new AssertionScope())
        {
            ReturnedActivity.Reference.Should().Be(Body.ActivityReference);
            ReturnedActivity.Title.Should().Be(Body.Title);
            ReturnedActivity.StartDate.Should().Be(Body.StartDate);
            ReturnedActivity.EndDate.Should().Be(Body.EndDate);
            ReturnedActivity.Note.Should().Be(Body.Note);
            ReturnedActivity.Completed.Should().Be(Body.Completed);
            ReturnedActivity.Type.Should().Be(Body.Type);
        }
    }

    [Fact]
    public async Task ThenUpdatesActivityInDatabase()
    {
        await using JobbyDbContext context = factory.GetDbContext();
        
        Activity updatedActivity = await context.Activities
            .SingleAsync(x => x.Id == PreLoadedActivity.Id);

        using (new AssertionScope())
        {
            updatedActivity.Title.Should().Be(Body.Title);
            updatedActivity.StartDate.Should().Be(Body.StartDate);
            updatedActivity.EndDate.Should().Be(Body.EndDate);
            updatedActivity.Note.Should().Be(Body.Note);
            updatedActivity.Completed.Should().Be(Body.Completed);
            updatedActivity.Type.Should().Be(Body.Type);
        }
    }
}