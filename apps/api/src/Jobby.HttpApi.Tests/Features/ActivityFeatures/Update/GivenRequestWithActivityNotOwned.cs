using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Update;

[Collection("SqlCollection")]
public class GivenRequestWithActivityNotOwned(JobbyHttpApiFactory factory)
{
    [Fact]
    public async Task ThenReturns401Unauthorized()
    {
        (_, Activity preLoadedActivity) = await SeedDataHelper.CreateBoardWithActivityAsync(factory, "RandomUserId");
        
        UpdateActivityCommand body = new(
            ActivityReference: preLoadedActivity.Reference,
            Title: "Test Activity",
            Type: (int)ActivityConstants.Types.Apply,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(1),
            Note: "Test Note",
            Completed: false);

        HttpResponseMessage response = await factory.HttpClient.PutAsJsonAsync("/activity", body);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        responseContent.Should().Be(ResponseHelper.MessageToApiMessage(
            $"You are not authorised to access the resource {preLoadedActivity.Reference}."));
    }
}