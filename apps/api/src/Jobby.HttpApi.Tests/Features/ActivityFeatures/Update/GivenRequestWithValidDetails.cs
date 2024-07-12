using System.Net;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Features.ActivityFeatures.Update.Fixtures;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Update;

[Collection("SqlCollection")]
public class GivenRequestWithValidDetails(
    UpdateActivityTestFixture fixture,
    JobbyHttpApiFactory factory)
    : IClassFixture<UpdateActivityTestFixture>
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