using System.Net;
using System.Text;
using Jobby.HttpApi.Tests.Factories;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class GivenRequestWithValidationErrors(JobbyHttpApiFactory factory)
{
    private HttpClient HttpClient => factory.SetupClient();

    [Fact]
    public async Task WhenTypePropertyIsInvalid_ThenReturns422UnprocessableEntityWithValidationMessage()
    {
        const string invalidType = 
        """
        {
          "title": "k",
          "boardId": "2d97a782-24a6-2962-e7cd-3a0c759c3d29",
          "type": 999,
          "startDate": "2023-08-15T23:00:00.000Z",
          "endDate": "2023-08-15T23:00:00.000Z",
          "note": "Cool note"
        }
        """;

        StringContent body = new(invalidType, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await HttpClient.PostAsync("/activity", body);
        
        string content = await response.Content.ReadAsStringAsync();

        content.Should().Contain("The provided activity type is not valid.");
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }
    
    [Fact]
    public async Task WhenBoardIdPropertyIsMissing_ThenReturns422UnprocessableEntityWithValidation_Message()
    {
        const string withoutBoardId = 
        """
        {
          "title": "k",
          "type": 12,
          "startDate": "2023-08-15T23:00:00.000Z",
          "endDate": "2023-08-15T23:00:00.000Z",
          "note": "Cool note"
        }
        """;

        StringContent body = new StringContent(withoutBoardId, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await HttpClient.PostAsync("/activity", body);
        
        string content = await response.Content.ReadAsStringAsync();
        
        content.Should().Contain("This property is required.");
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }
    
    [Fact]
    public async Task WhenStartDatePropertyIsMissing_ThenReturns422UnprocessableEntityWithValidation_Message()
    {
        const string withoutStartDate = 
        """
        {
          "title": "k",
          "type": 12,
          "boardId": "2d97a782-24a6-2962-e7cd-3a0c759c3d29",
          "endDate": "2023-08-15T23:00:00.000Z",
          "note": "test note"
        }
        """;

        StringContent body = new StringContent(withoutStartDate, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await HttpClient.PostAsync("/activity", body);
        
        string content = await response.Content.ReadAsStringAsync();

        content.Should().Contain("This property is required.");
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }
}