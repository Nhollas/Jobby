using System.Net.Http.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create.Fixtures;

public class ValidDetailsTestFixture : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public ValidDetailsTestFixture(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }
    
    public HttpResponseMessage Response { get; private set; } = new();
    public ActivityDto? ReturnedActivity { get; private set; } = new();

    private HttpClient HttpClient => _factory.SetupClient();
    
    public CreateActivityCommand Body { get; private set; } = new();

    private const string UserId = "TestUserId";


    public static readonly Board PreloadedBoard = Board.Create(Guid.NewGuid(), DateTime.UtcNow, UserId, "TestBoard");
    
    public async Task InitializeAsync()
    {
        await using var initContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        await SeedDataHelper<Board>.AddAsync(PreloadedBoard, initContext);
        
        Body = new CreateActivityCommand
        {
            BoardReference = PreloadedBoard.Reference,
            Title = "Test Activity",
            Type = ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false
        };
        
        Response = await HttpClient.PostAsJsonAsync("/activity", Body);
        ReturnedActivity = await Response.Content.ReadFromJsonAsync<ActivityDto>();
    }

    public async Task DisposeAsync()
    {
        await using var disposeContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        await SeedDataHelper<Board>.RemoveAsync(PreloadedBoard, disposeContext);
    }
}