using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Update.Fixtures;

public class UpdateActivityTestFixture : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public UpdateActivityTestFixture(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    public HttpResponseMessage Response { get; private set; } = new();

    private HttpClient HttpClient => _factory.SetupClient();
    
    private static string _userId = "TestUserId";

    public static readonly Board PreloadedBoard = Board.Create(Guid.NewGuid(), DateTime.UtcNow, _userId, "TestBoard");
    
    public UpdateActivityCommand Body { get; private set; } = new();
    
    public Activity PreloadedActivity = Activity.Create(
        Guid.NewGuid(),
        DateTime.UtcNow,
        _userId,
        "TestActivity",
        (int)ActivityConstants.Types.Apply,
        DateTime.UtcNow,
        DateTime.UtcNow.AddDays(1),
        "Test Note",
        false,
        PreloadedBoard
    );


    public async Task InitializeAsync()
    {
        await using var initContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        await SeedDataHelper<Board>.AddAsync(PreloadedBoard, initContext);
        await SeedDataHelper<Activity>.AddAsync(PreloadedActivity, initContext);
        
        Body = new UpdateActivityCommand()
        {
            ActivityReference = PreloadedActivity.Reference,
            Title = "Test Activity",
            Type = (int)ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false
        };
        
        Response = await HttpClient.PutAsJsonAsync("/activity", Body);
    }

    public async Task DisposeAsync()
    {
        await using var disposeContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        await SeedDataHelper<Board>.RemoveAsync(PreloadedBoard, disposeContext);
    }
}