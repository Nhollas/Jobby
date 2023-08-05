using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.Activity.Create;

[Collection("SqlCollection")]
public class Given_Request_With_Valid_Details : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_Valid_Details(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    private HttpClient HttpClient => _factory.HttpClient;
    

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Then_Returns_201_Created_And_Activity_Is_Stored()
    {
        var body = new CreateActivityCommand()
        {
            BoardId = Guid.Parse("b0630723-78d9-4ba3-a825-88714e15aa2c"),
            Title = "Test Activity",
            Type = ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false
        };

        var response = await HttpClient.PostAsJsonAsync("/api/activity/create", body);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}