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
    public ActivityDto? ReturnedActivity { get; private set; }
    public UpdateActivityCommand Body { get; private set; } = null!;
    public Activity PreloadedActivity { get; private set; } = null!;


    public async Task InitializeAsync()
    {
        (_, Activity preloadedActivity) = await SeedDataHelper.CreateBoardWithActivityAsync(factory);
        PreloadedActivity = preloadedActivity;

        Body = new UpdateActivityCommand(
            ActivityReference: PreloadedActivity.Reference,
            Title: "Test Activity",
            Type: (int)ActivityConstants.Types.Apply,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(1),
            Note: "Test Note",
            Completed: false);
        
        Response = await factory.HttpClient.PutAsJsonAsync("/activity", Body);
        ReturnedActivity = await Response.Content.ReadFromJsonAsync<ActivityDto>();
    }

    public Task DisposeAsync() => Task.CompletedTask;
}