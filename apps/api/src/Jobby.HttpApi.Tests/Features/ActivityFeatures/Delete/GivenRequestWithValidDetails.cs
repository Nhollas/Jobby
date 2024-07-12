using System.Net;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete.Fixtures;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class GivenRequestWithValidDetails(
    JobbyHttpApiFactory factory,
    DeleteActivityWithValidDetailsFixture fixture)
    : IClassFixture<DeleteActivityWithValidDetailsFixture>
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