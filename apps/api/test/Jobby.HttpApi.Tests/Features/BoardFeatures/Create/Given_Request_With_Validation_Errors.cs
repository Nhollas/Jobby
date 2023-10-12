using System.Net;
using System.Net.Http.Json;
using System.Text;
using Jobby.HttpApi.Tests.Factories;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.BoardFeatures.Create;

[Collection("SqlCollection")]
public class Given_Request_With_Validation_Errors : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_Validation_Errors(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }
    
    private HttpClient HttpClient => _factory.SetupClient();
    

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task When_Name_Property_Is_Missing_Then_Returns_422_Unprocessable_Entity_And_Name_Property_Validation_Message()
    {
        const string withoutName = "{}";

        var body = new StringContent(withoutName, Encoding.UTF8, "application/json");
        
        var response = await HttpClient.PostAsync("/board", body);
        
        var errors = await response.Content.ReadFromJsonAsync<List<ValidationError>>();
        
        Assert.NotNull(errors);
        Assert.Equal("Name", errors[0].PropertyName);
        Assert.Equal("Name is required.", errors[0].Message);
        
        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }
}