using System.Net;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.HttpApi.Tests.Setup;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class GivenRequestWithUnknownActivityWhenDeletingActivity(JobbyHttpApiFactory factory)
{
    [Fact]
    public async Task ThenReturns404NotFound()
    {
        string randomActivityRef = Guid.NewGuid().ToString();

        HttpResponseMessage response = await factory.HttpClient.DeleteAsync($"/activity/{randomActivityRef}");
        string responseContent = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        responseContent.Should().Be(ResponseHelper.MessageToApiMessage($"The Activity with Reference {randomActivityRef} could not be found."));
    }
}