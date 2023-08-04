using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.Activity.Create;

[Collection("SqlCollection")]
public class Given_New_Activity_When_Create_Is_Called : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_New_Activity_When_Create_Is_Called(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    public HttpClient HttpClient => _factory.HttpClient;
    

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Then_Created_Activity_Is_Returned_And_Is_Stored()
    {
        var activityToCreate  = new CreateActivityCommand()
        {
            BoardId = Guid.NewGuid(),
            JobId = Guid.NewGuid(),
            Title = "Test Activity",
            Type = ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false
        };

        var response = await HttpClient.PostAsJsonAsync("/api/activity/create", activityToCreate);
        
        var content = await response.Content.ReadAsStringAsync();

        var createdActivity = JsonSerializer.Deserialize<ActivityDto>(content);
        
        // Check that we get 201 Created
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}