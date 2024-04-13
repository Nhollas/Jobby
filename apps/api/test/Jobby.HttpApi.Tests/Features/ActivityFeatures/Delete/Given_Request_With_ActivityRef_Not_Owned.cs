using System.Net;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class GivenRequestWithActivityRefNotOwned
{
    private readonly JobbyHttpApiFactory _factory;

    public GivenRequestWithActivityRefNotOwned(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    private HttpClient HttpClient => _factory.SetupClient();
    
    [Fact]
    public async Task Then_Returns_404_Unauthorized()
    {
        await using JobbyDbContext context = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        Board preLoadedBoard = await SeedDataHelper<Board>.AddAsync(Board.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserTwoId", "TestBoard"), context);
        
        Activity? activityToDelete = Activity.Create(
            id: Guid.NewGuid(),
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
        
        HttpResponseMessage response = await HttpClient.DeleteAsync($"/activity/{preLoadedActivity.Reference}");
        string responseContent = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        responseContent.Should().Be("You are not authorised to access this resource.");
    }
}