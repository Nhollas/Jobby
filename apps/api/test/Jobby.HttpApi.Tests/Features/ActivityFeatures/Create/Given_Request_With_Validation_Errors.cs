using System.Net;
using System.Text;
using Jobby.HttpApi.Tests.Factories;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class Given_Request_With_Validation_Errors : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_Validation_Errors(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    private HttpClient HttpClient => _factory.SetupClient();
    

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task When_Type_Property_Is_Invalid_Then_Returns_422_Unprocessable_Entity_And_Type_Property_Validation_Message()
    {
        const string json = """
                            {
                              "title": "k",
                              "boardId": "2d97a782-24a6-2962-e7cd-3a0c759c3d29",
                              "type": 41,
                              "startDate": "2023-08-15T23:00:00.000Z",
                              "endDate": "2023-08-15T23:00:00.000Z",
                              "note": "jhakhsdjksad"
                            }
                            """;

        var body = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await HttpClient.PostAsync("/api/activity", body);
        
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("The provided activity type is not valid.", content);
        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }
    
    [Fact]
    public async Task When_BoardId_Property_Is_Missing_Then_Returns_422_Unprocessable_Entity_And_BoardId_Property_Validation_Message()
    {
        const string withoutBoardId = """
                            {
                              "title": "k",
                              "type": 12,
                              "startDate": "2023-08-15T23:00:00.000Z",
                              "endDate": "2023-08-15T23:00:00.000Z",
                              "note": "jhakhsdjksad"
                            }
                            """;

        var body = new StringContent(withoutBoardId, Encoding.UTF8, "application/json");
        
        var response = await HttpClient.PostAsync("/api/activity", body);
        
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("This property is required.", content);
        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }
    
    [Fact]
    public async Task When_StartDate_Property_Is_Missing_Then_Returns_422_Unprocessable_Entity_And_StartDate_Property_Validation_Message()
    {
        const string json = """
                            {
                              "title": "k",
                              "type": 12,
                              "boardId": "2d97a782-24a6-2962-e7cd-3a0c759c3d29",
                              "endDate": "2023-08-15T23:00:00.000Z",
                              "note": "jhakhsdjksad"
                            }
                            """;

        var body = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await HttpClient.PostAsync("/api/activity", body);
        
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("This property is required.", content);
        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }
}