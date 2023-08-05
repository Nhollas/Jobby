using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.Activity.Create;

[Collection("SqlCollection")]
public class Given_Request_With_BoardId_Not_Owned : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_BoardId_Not_Owned(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    private HttpClient HttpClient => _factory.HttpClient;
    

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Then_Returns_403_Forbidden()
    {
        var user2BoardId = Guid.Parse("01685b73-0b18-4ef7-a358-37c13f254a28");
        
        var body = new CreateActivityCommand()
        {
            BoardId = user2BoardId,
            Title = "Test Activity",
            Type = ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false
        };

        var response = await HttpClient.PostAsJsonAsync("/api/activity/create", body);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}