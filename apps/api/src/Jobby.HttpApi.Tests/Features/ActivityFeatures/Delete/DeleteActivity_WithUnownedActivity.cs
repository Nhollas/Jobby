using System.Net;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.HttpApi.Tests.Setup;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class DeleteActivity_WithUnownedActivity(JobbyHttpApiFactory factory)
{
    [Fact]
    public async Task ThenReturns404Unauthorized()
    {
        Board board = await new TestDataBuilder(factory)
            .CreateBoard(userId: "TestUser2Id")
            .WithActivity()
            .SeedAsync();
        
        Activity activity = board.Activities.First();
        
        
        HttpResponseMessage response = await factory.HttpClient.DeleteAsync($"/activity/{activity.Reference}");
        string responseContent = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        responseContent.Should()
            .Be(ResponseHelper.MessageToApiMessage(
                $"You are not authorised to access the resource {activity.Reference}."));

    }
}