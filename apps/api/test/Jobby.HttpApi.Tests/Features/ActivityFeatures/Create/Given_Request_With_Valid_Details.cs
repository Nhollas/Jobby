using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class Given_Request_With_Valid_Details : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_Valid_Details(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    private HttpClient HttpClient => _factory.SetupClient();
    
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Then_Returns_201_Created_And_Activity_Is_Stored()
    {
        await using var context = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        var preLoadedBoard = await SeedDataHelper<Board>.AddAsync(Board.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "TestBoard", new List<JobList>()), context);
        
        var body = new CreateActivityCommand()
        {
            BoardId = preLoadedBoard.Id,
            Title = "Test Activity",
            Type = ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false
        };

        var response = await HttpClient.PostAsJsonAsync("/api/activity/create", body);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}