using System.Net.Http.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Update.Fixtures;

public class UpdateActivityTestFixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    
    public ActivityDto? ReturnedActivity { get; private set; } = new();
    private HttpClient HttpClient => factory.SetupClient();

    private const string UserId = "TestUserId";

    private static readonly Board PreloadedBoard = Board.Create(Guid.NewGuid(), DateTime.UtcNow, UserId, "TestBoard");
    
    public UpdateActivityCommand Body { get; private set; } = new();
    
    public readonly Activity PreloadedActivity = Activity.Create(
        Guid.NewGuid(),
        DateTime.UtcNow,
        UserId,
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
        await using JobbyDbContext context = factory.GetDbContext();
        
        await SeedDataHelper<Board>.AddAsync(PreloadedBoard, context);
        await SeedDataHelper<Activity>.AddAsync(PreloadedActivity, context);
        
        Body = new UpdateActivityCommand
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
        ReturnedActivity = await Response.Content.ReadFromJsonAsync<ActivityDto>();
    }

    public async Task DisposeAsync()
    {
        await using JobbyDbContext context = factory.GetDbContext();
        
        await SeedDataHelper<Board>.RemoveAsync(PreloadedBoard, context);
    }
}