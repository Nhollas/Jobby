using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class Given_Request_With_Job_To_Link : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_Job_To_Link(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    private HttpClient HttpClient => _factory.SetupClient();
    
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Then_Returns_201_Created_And_Activity_With_Job_Is_Stored()
    {
        await using var context = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        var boardId = Guid.NewGuid();
        
        var board = Board.Create(boardId, DateTime.UtcNow, "TestUserId", "TestBoard");
        var preLoadedJobList = JobList.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "TestJobList", 0, board);
        var preLoadedBoard = await SeedDataHelper<Board>.AddAsync(board, context);
        var preLoadedJob = await SeedDataHelper<Job>.AddAsync(Job.Create(Guid.NewGuid(), DateTime.UtcNow, "TestUserId", "TestCompany", "TestTitle", 0, preLoadedJobList, preLoadedBoard), context);
        
        var body = new CreateActivityCommand()
        { 
            BoardReference = preLoadedBoard.Reference,
            Title = "Test Activity",
            JobReference = preLoadedJob.Reference,
            Type = ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false
        };

        var response = await HttpClient.PostAsJsonAsync("/activity", body);
        
        var createdActivity = await response.Content.ReadFromJsonAsync<ActivityDto>();

        Assert.NotNull(createdActivity);
        
        Assert.Equal(preLoadedJob.Reference, createdActivity.Job.Reference);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}