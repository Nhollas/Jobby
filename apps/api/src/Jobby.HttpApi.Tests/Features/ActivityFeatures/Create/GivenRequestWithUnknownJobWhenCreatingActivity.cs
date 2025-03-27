using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.HttpApi.Tests.Setup;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class GivenRequestWithUnknownJobWhenCreatingActivity(JobbyHttpApiFactory factory)
{
    [Fact]
    public async Task ThenReturns400BadRequest()
    {
        Board board = await new TestDataBuilder(factory)
            .CreateBoard()
            .WithJob()
            .SeedAsync();
        
        CreateActivityCommand body = new(
            BoardReference: board.Reference,
            Title: "Test Activity",
            Type: ActivityConstants.Types.Apply,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddDays(1),
            Note: "Test Note",
            JobReference: "UnknownJobReference",
            Completed: false
        );

        HttpResponseMessage response = await factory.HttpClient.PostAsJsonAsync("/activity", body);
        string responseContent = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseContent.Should().Be(
            ResponseHelper.MessageToApiMessage($"The Job '{body.JobReference}' you wanted to link doesn't exist in the Board '{body.BoardReference}'."));
    }
}