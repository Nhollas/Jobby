using System.Net;
using Jobby.HttpApi.Tests.Factories;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class Given_Request_With_ActivityRef_Not_Found
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_ActivityRef_Not_Found(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    private HttpClient HttpClient => _factory.SetupClient();
    
    [Fact]
    public async Task Then_Returns_404_NotFound()
    {
        var randomActivityRef = Guid.NewGuid().ToString();

        var response = await HttpClient.DeleteAsync($"/activity/{randomActivityRef}");
        var responseContent = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        responseContent.Should().Be($"The Activity with Reference {randomActivityRef} could not be found.");
    }
}