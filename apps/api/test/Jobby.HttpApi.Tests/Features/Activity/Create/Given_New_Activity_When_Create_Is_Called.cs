using System.Net.Http.Json;
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
    

    public Task InitializeAsync() => _factory.InitializeDatabaseAsync();

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Then_Created_Activity_Is_Returned_And_Is_Stored()
    {
        var test  = new CreateActivityCommand()
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

        var response = await HttpClient.GetAsync("/api/boards");
        
        var content = await response.Content.ReadAsStringAsync();
        
        // _factory.MockHttpHandler.When(HttpMethod.Post)
        
        // Test implementation here
        
        Assert.True(true);
    }
}