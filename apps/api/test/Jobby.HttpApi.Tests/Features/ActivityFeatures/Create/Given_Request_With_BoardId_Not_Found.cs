using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class GivenRequestWithBoardIdNotFound
{
    private readonly JobbyHttpApiFactory _factory;

    public GivenRequestWithBoardIdNotFound(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    private HttpClient HttpClient => _factory.SetupClient();
    
    [Fact]
    public async Task Then_Returns_404_NotFound()
    {
        string? randomBoardReference = EntityReferenceProvider<Board>.CreateReference();
        
        CreateActivityCommand body = new CreateActivityCommand
        {
            BoardReference = randomBoardReference,
            Title = "Test Activity",
            Type = ActivityConstants.Types.Apply,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Note = "Test Note",
            Completed = false
        };

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/activity", body);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        responseContent.Should().Be($"The Board with Reference {body.BoardReference} could not be found.");
    }
}