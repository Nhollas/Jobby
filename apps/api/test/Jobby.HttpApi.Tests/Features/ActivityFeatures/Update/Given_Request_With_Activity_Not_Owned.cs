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
public class Given_Request_With_Activity_Not_Owned
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_Activity_Not_Owned(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }
    
    private HttpClient HttpClient => _factory.SetupClient();
    
    [Fact]
    public async Task Then_Returns_401_Unauthorized()
    {
        await using var context = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        var preLoadedBoard = Board.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "TestBoard");
        
        await SeedDataHelper<Board>.AddAsync(preLoadedBoard, context);

        var preLoadedActivity = Activity.Create(
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
        
        var body = new UpdateActivityCommand()
        {
            ActivityReference = preLoadedActivity.Reference,
            Title = "Test Activity",
            Type = (int)ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false
        };

        var response = await HttpClient.PutAsJsonAsync("/activity", body);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        responseContent.Should().Be("You are not authorised to access this resource.");
    }
}