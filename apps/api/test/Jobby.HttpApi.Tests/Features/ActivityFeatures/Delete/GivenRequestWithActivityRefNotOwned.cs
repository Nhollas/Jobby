using System.Net;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class GivenRequestWithActivityRefNotOwned(JobbyHttpApiFactory factory)
{
    [Fact]
    public async Task ThenReturns404Unauthorized()
    {
        await using JobbyDbContext context = factory.GetDbContext();
        
        Board preLoadedBoard = await SeedDataHelper<Board>.AddAsync(Board.Create(DateTime.UtcNow, "TestUserTwoId", "TestBoard"), context);
        
        Activity activityToDelete = Activity.Create(
            createdDate: DateTime.UtcNow,
            ownerId: "TestUserTwoId",
            title: "Test Activity",
            activityType: 1,
            startDate: DateTime.UtcNow,
            endDate: DateTime.UtcNow,
            note: "Test Note",
            completed: false,
            board: preLoadedBoard
        );
        
        Activity preLoadedActivity = await SeedDataHelper<Activity>.AddAsync(activityToDelete, context);
        
        HttpResponseMessage response = await factory.HttpClient.DeleteAsync($"/activity/{preLoadedActivity.Reference}");
        string responseContent = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        responseContent.Should().Be("You are not authorised to access this resource.");
    }
}