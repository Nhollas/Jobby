using System.Net.Http.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create.Fixtures;

public class ValidDetailsTestFixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    public ActivityDto? ReturnedActivity { get; private set; }
    public CreateActivityCommand Body { get; private set; } = null!;
    
    private static readonly Board SeededBoard = Board.Create(
        createdDate: DateTime.UtcNow, 
        ownerId: "TestUserId",
        name: "TestBoard");
    
    public async Task InitializeAsync()
    {
        await using JobbyDbContext dbContext = factory.GetDbContext();
        
        await SeedDataHelper<Board>.AddAsync(SeededBoard, dbContext);

        Body = new CreateActivityCommand(
            BoardReference: SeededBoard.Reference,
            Title: "Test Activity",
            Type: ActivityConstants.Types.Apply,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(1),
            Note: "Test Note",
            Completed: false
        );
        
        Response = await factory.HttpClient.PostAsJsonAsync("/activity", Body);
        ReturnedActivity = await Response.Content.ReadFromJsonAsync<ActivityDto>();
    }

    public async Task DisposeAsync()
    {
        await using JobbyDbContext dbContext = factory.GetDbContext();
        
        await SeedDataHelper<Board>.RemoveAsync(SeededBoard, dbContext);
    }
}