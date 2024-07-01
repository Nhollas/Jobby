using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class GivenRequestWithBoardIdNotFound(JobbyHttpApiFactory factory)
{
    [Fact]
    public async Task ThenReturns404NotFound()
    {
        string randomBoardReference = EntityReferenceProvider<Board>.CreateReference();

        CreateActivityCommand body = new(
            BoardReference: randomBoardReference,
            Title: "Test Activity",
            Type: ActivityConstants.Types.Apply,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(1),
            Note: "Test Note",
            Completed: false
        );

        HttpResponseMessage response = await factory.HttpClient.PostAsJsonAsync("/activity", body);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        responseContent.Should().Be(
            ResponseHelper.MessageToApiMessage(body.BoardReference));
    }
}