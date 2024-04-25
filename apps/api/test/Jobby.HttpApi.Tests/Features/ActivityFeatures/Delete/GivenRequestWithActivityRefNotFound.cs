using System.Net;
using Jobby.HttpApi.Tests.Factories;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class GivenRequestWithActivityRefNotFound(JobbyHttpApiFactory factory)
{
    private HttpClient HttpClient => factory.SetupClient();
    
    [Fact]
    public async Task ThenReturns404NotFound()
    {
        string randomActivityRef = Guid.NewGuid().ToString();

        HttpResponseMessage response = await HttpClient.DeleteAsync($"/activity/{randomActivityRef}");
        string responseContent = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        responseContent.Should().Be($"The Activity with Reference {randomActivityRef} could not be found.");
    }
}