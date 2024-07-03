using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class GivenRequestWithUnknownJob(JobbyHttpApiFactory factory)
{
    [Fact]
    public async Task ThenReturns400BadRequest()
    {
        await using JobbyDbContext context = factory.GetDbContext();
        
        Board preLoadedBoard = await SeedDataHelper.AddAsync(Board.Create(DateTime.UtcNow, "TestUserId", "TestBoard"), context);
        
        CreateActivityCommand body = new(
            BoardReference: preLoadedBoard.Reference,
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
            ResponseHelper.MessageToApiMessage($"The Job {body.JobReference} you wanted to link doesn't exist in the Board {body.BoardReference}."));
    }
}