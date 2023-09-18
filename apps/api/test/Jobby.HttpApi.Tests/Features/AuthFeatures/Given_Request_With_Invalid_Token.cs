using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.AuthFeatures;

[Collection("SqlCollection")]
public class Given_Request_With_Invalid_Token : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public Given_Request_With_Invalid_Token(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task When_Token_Has_Expired_Then_Returns_401_Unauthorized()
    {
        var httpClient = _factory.CreateClient();
        
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

        var expiredToken = JwtHelper.Generate("TestUserId", expires: DateTime.UtcNow.AddDays(-1));

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", expiredToken);

        var body = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The token expired at '{DateTime.UtcNow.AddDays(-1).ToString("MM/dd/yyyy HH:mm:ss")}'\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task When_Token_Has_Invalid_Signature_Then_Returns_401_Unauthorized()
    {
        var httpClient = _factory.CreateClient();
        
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

        var invalidSecret = "Q5BkcNr4WncjhC1RiqwXmMF4zdFttEab";
        var token = JwtHelper.Generate("TestUserId", secret: invalidSecret);

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var body = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The signature key was not found\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task When_Token_Has_Invalid_Issuer_Then_Returns_401_Unauthorized()
    {
        var httpClient = _factory.CreateClient();
        
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

        var invalidIssuer = "InvalidIssuer";
        var token = JwtHelper.Generate("TestUserId", issuer: invalidIssuer);

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var body = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The issuer 'InvalidIssuer' is invalid\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task When_Token_Has_Invalid_Audience_Then_Returns_401_Unauthorized()
    {
        var httpClient = _factory.CreateClient();
        
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

        var invalidAudience = "InvalidAudience";
        var token = JwtHelper.Generate("TestUserId", audience: invalidAudience);

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var body = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The audience 'InvalidAudience' is invalid\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}