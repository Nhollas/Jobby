using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Update;

[Collection("SqlCollection")]
public class GivenRequestWithActivityNotOwned(JobbyHttpApiFactory factory)
{
    private HttpClient HttpClient => factory.SetupClient();
    
    [Fact]
    public async Task ThenReturns401Unauthorized()
    {
        await using JobbyDbContext context = factory.GetDbContext();
        
        Board? preLoadedBoard = Board.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "TestBoard");
        
        await SeedDataHelper<Board>.AddAsync(preLoadedBoard, context);

        Activity? preLoadedActivity = Activity.Create(
            Guid.NewGuid(),
            DateTime.UtcNow,
            "TestUser2Id",
            "TestActivity",
            (int)ActivityConstants.Types.Apply,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            "Test Note",
            false,
            preLoadedBoard
        );
        
        await SeedDataHelper<Activity>.AddAsync(preLoadedActivity, context);
        
        UpdateActivityCommand body = new()
        {
            ActivityReference = preLoadedActivity.Reference,
            Title = "Test Activity",
            Type = (int)ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false
        };

        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("/activity", body);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        responseContent.Should().Be("You are not authorised to access this resource.");
    }
}