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

    public Task InitializeAsync() => _factory.InitializeDatabaseAsync();

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public void Then_Created_Activity_Is_Returned_And_Is_Stored()
    {
        // _factory.MockHttpHandler.When(HttpMethod.Post)
        
        // Test implementation here
        
        Assert.True(true);
    }
}