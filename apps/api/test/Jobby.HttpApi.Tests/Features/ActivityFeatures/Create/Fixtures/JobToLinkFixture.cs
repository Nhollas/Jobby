using System.Net.Http.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create.Fixtures;

public class JobToLinkFixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    public ActivityDto? ReturnedActivity { get; private set; }
    public CreateActivityCommand Body { get; private set; } = null!;
    
    public async Task InitializeAsync()
    {
        await using JobbyDbContext dbContext = factory.GetDbContext();

        Board seededBoard = await SeedBundleHelper.AddBoardWithJobAsync(dbContext);
        
        Job seededJob = seededBoard.Lists.First().Jobs.First();
        
        Body = new CreateActivityCommand(
            BoardReference: seededBoard.Reference,
            Title: "Test Activity",
            Type: ActivityConstants.Types.Apply,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(1),
            Note: "Test Note",
            Completed: false,
            JobReference: seededJob.Reference
        );
        
        Response = await factory.CreateClient().PostAsJsonAsync("/activity", Body);
        ReturnedActivity = await Response.Content.ReadFromJsonAsync<ActivityDto>();
    }

    public Task DisposeAsync() => Task.CompletedTask;
}