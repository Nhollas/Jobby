using System.Net;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Features.ActivityFeatures.Create.Fixtures;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class GivenRequestWithValidDetails(
    JobbyHttpApiFactory factory,
    ValidDetailsTestFixture fixture)
    : IClassFixture<ValidDetailsTestFixture>
{
    private const string ExpectedName = "Apply";
    
    [Fact]
    public void ThenReturns201Created() => 
        fixture.Response.StatusCode.Should().Be(HttpStatusCode.Created);

    [Fact]
    public void ThenReturnsCreatedActivity()
    {
        using (new AssertionScope())
        {
            fixture.ReturnedActivity.Title.Should().Be(fixture.Body.Title);
            fixture.ReturnedActivity.Name.Should().Be(ExpectedName);
            fixture.ReturnedActivity.StartDate.Should().Be(fixture.Body.StartDate);
            fixture.ReturnedActivity.EndDate.Should().Be(fixture.Body.EndDate);
            fixture.ReturnedActivity.Note.Should().Be(fixture.Body.Note);
            fixture.ReturnedActivity.Completed.Should().Be(fixture.Body.Completed);
            fixture.ReturnedActivity.BoardReference.Should().Be(fixture.Body.BoardReference);
            fixture.ReturnedActivity.Job.Should().BeNull();
            fixture.ReturnedActivity.Type.Should().Be((int)fixture.Body.Type);
        }
    }

    [Fact]
    public async Task ThenInsertsActivityInDatabase()
    {
        await using JobbyDbContext context = factory.GetDbContext();

        Activity createdActivity = await context.Activities
            .Include(activity => activity.Job)
            .SingleAsync(activity => activity.Reference == fixture.ReturnedActivity.Reference);
        
        using (new AssertionScope())
        {
            createdActivity.Title.Should().Be(fixture.Body.Title);
            createdActivity.Name.Should().Be(ExpectedName);
            createdActivity.StartDate.Should().Be(fixture.Body.StartDate);
            createdActivity.EndDate.Should().Be(fixture.Body.EndDate);
            createdActivity.Note.Should().Be(fixture.Body.Note);
            createdActivity.Completed.Should().Be(fixture.Body.Completed);
            createdActivity.BoardReference.Should().Be(fixture.Body.BoardReference);
            createdActivity.Job.Should().BeNull();
            createdActivity.Type.Should().Be((int)fixture.Body.Type);
        }
    }
}