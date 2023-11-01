using System.Net.Http.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create.Fixtures;

public class JobToLinkFixture : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public JobToLinkFixture(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }
    
    public HttpResponseMessage Response { get; private set; } = new();
    
    public ActivityDto? ReturnedActivity { get; private set; } = new();
    
    private HttpClient HttpClient => _factory.SetupClient();
    
    public CreateActivityCommand Body { get; private set; } = new();
    
    public async Task InitializeAsync()
    {
        await using var initContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);

        var preLoadedBoard = await SeedBundleHelper.AddBoardWithJobAsync(initContext);
        
        var preloadedJob = preLoadedBoard.Lists.First().Jobs.First();
        
        Body = new CreateActivityCommand
        {
            BoardReference = preLoadedBoard.Reference,
            Title = "Test Activity",
            Type = ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false,
            JobReference = preloadedJob.Reference
        };
        
        Response = await HttpClient.PostAsJsonAsync("/activity", Body);
        ReturnedActivity = await Response.Content.ReadFromJsonAsync<ActivityDto>();
    }
    
    public async Task DisposeAsync()
    {
        await using var disposeContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
    }
}