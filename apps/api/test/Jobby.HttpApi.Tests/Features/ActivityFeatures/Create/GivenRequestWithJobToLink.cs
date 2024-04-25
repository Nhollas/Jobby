using System.Net;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Features.ActivityFeatures.Create.Fixtures;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class GivenRequestWithJobToLink(
    JobbyHttpApiFactory factory,
    JobToLinkFixture fixture) : IClassFixture<JobToLinkFixture>
{
    private HttpResponseMessage Response => fixture.Response;
    private CreateActivityCommand Body => fixture.Body;
    private ActivityDto ReturnedActivity => fixture.ReturnedActivity!;
    
    private static string ExpectedName => "Apply";
    
    [Fact]
    public void ThenReturns201Created()
    {
        Response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public void ThenReturnsCreatedActivityWithJobLinked()
    {
        using (new AssertionScope())
        {
            ReturnedActivity.Title.Should().Be(Body.Title);
            ReturnedActivity.Name.Should().Be(ExpectedName);
            ReturnedActivity.StartDate.Should().Be(Body.StartDate);
            ReturnedActivity.EndDate.Should().Be(Body.EndDate);
            ReturnedActivity.Note.Should().Be(Body.Note);
            ReturnedActivity.Completed.Should().Be(Body.Completed);
            ReturnedActivity.BoardReference.Should().Be(Body.BoardReference);
            ReturnedActivity.Job.Reference.Should().Be(Body.JobReference);
            ReturnedActivity.Type.Should().Be((int)Body.Type);
        }
    }
    
    [Fact]
    public async Task ThenInsertsActivityInDatabaseAndHasJobLinked()
    {
        await using JobbyDbContext context = factory.GetDbContext();
        
        Activity createdActivity = await context.Activities
            .Include(activity => activity.Job)
            .SingleAsync(activity => activity.Reference == ReturnedActivity.Reference);
        
        using (new AssertionScope())
        {
            createdActivity.Title.Should().Be(Body.Title);
            createdActivity.Name.Should().Be(ExpectedName);
            createdActivity.StartDate.Should().Be(Body.StartDate);
            createdActivity.EndDate.Should().Be(Body.EndDate);
            createdActivity.Note.Should().Be(Body.Note);
            createdActivity.Completed.Should().Be(Body.Completed);
            createdActivity.BoardReference.Should().Be(Body.BoardReference);
            createdActivity.Job.Reference.Should().Be(Body.JobReference);
            createdActivity.Type.Should().Be((int)Body.Type);
        }
    }
}