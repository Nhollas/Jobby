using System.Net;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
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
    private HttpResponseMessage Response => fixture.Response;
    private CreateActivityCommand Body => fixture.Body;
    private ActivityDto ReturnedActivity => fixture.ReturnedActivity!;
    private string ExpectedName => ActivityConstants.TypesDictionary.GetValueOrDefault((int)Body.Type, ActivityConstants.TypesDictionary[0]);
    
    [Fact]
    public void ThenReturns201Created() => 
        Response.StatusCode.Should().Be(HttpStatusCode.Created);

    [Fact]
    public void ThenReturnsCreatedActivity()
    {
        Assert.Multiple(
            () => ReturnedActivity.Title.Should().Be(Body.Title),
            () => ReturnedActivity.Name.Should().Be(ExpectedName),
            () => ReturnedActivity.StartDate.Should().Be(Body.StartDate),
            () => ReturnedActivity.EndDate.Should().Be(Body.EndDate),
            () => ReturnedActivity.Note.Should().Be(Body.Note),
            () => ReturnedActivity.Completed.Should().Be(Body.Completed),
            () => ReturnedActivity.BoardReference.Should().Be(Body.BoardReference),
            () => ReturnedActivity.Job.Should().BeNull(),
            () => ReturnedActivity.Type.Should().Be((int)Body.Type)
        );
    }

    [Fact]
    public async Task ThenInsertsActivityInDatabase()
    {
        await using JobbyDbContext dbContext = factory.GetDbContext();

        Activity createdActivity = await dbContext.Activities
            .Include(activity => activity.Job)
            .SingleAsync(activity => activity.Reference == ReturnedActivity.Reference);
        
        Assert.Multiple(
            () => createdActivity.Title.Should().Be(Body.Title),
            () => createdActivity.Name.Should().Be(ExpectedName),
            () => createdActivity.StartDate.Should().Be(Body.StartDate),
            () => createdActivity.EndDate.Should().Be(Body.EndDate),
            () => createdActivity.Note.Should().Be(Body.Note),
            () => createdActivity.Completed.Should().Be(Body.Completed),
            () => createdActivity.BoardReference.Should().Be(Body.BoardReference),
            () => createdActivity.Job.Should().BeNull(),
            () => createdActivity.Type.Should().Be((int)Body.Type)
        );
    }
}