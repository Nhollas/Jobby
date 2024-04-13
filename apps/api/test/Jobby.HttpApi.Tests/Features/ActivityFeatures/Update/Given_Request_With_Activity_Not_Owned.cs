using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Update;

[Collection("SqlCollection")]
public class GivenRequestWithActivityNotOwned
{
    private readonly JobbyHttpApiFactory _factory;

    public GivenRequestWithActivityNotOwned(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }
    
    private HttpClient HttpClient => _factory.SetupClient();
    
    [Fact]
    public async Task Then_Returns_401_Unauthorized()
    {
        await using JobbyDbContext context = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
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
        
        UpdateActivityCommand body = new UpdateActivityCommand()
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