using System.Net;
using Jobby.HttpApi.Tests.Factories;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class Given_Request_With_ActivityId_Not_Found : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_ActivityId_Not_Found(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    private HttpClient HttpClient => _factory.HttpClient;
    

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Then_Returns_404_NotFound()
    {
        var randomActivityId = Guid.NewGuid();

        var response = await HttpClient.DeleteAsync($"/api/activity/delete/{randomActivityId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}