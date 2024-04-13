using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.AuthFeatures;

[Collection("SqlCollection")]
public class GivenRequestWithInvalidToken : IAsyncLifetime
{
    private readonly JobbyHttpApiFactory _factory;

    public GivenRequestWithInvalidToken(JobbyHttpApiFactory factory)
    {
        _factory = factory;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;

    private HttpClient SetupClient(string token)
    {
        HttpClient httpClient = _factory.CreateClient();
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        return httpClient;
    }

    [Fact]
    public async Task When_Token_Has_Expired_Then_Returns_401_Unauthorized()
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

        string expiredToken = JwtHelper.Generate("TestUserId", expires: DateTime.UtcNow.AddDays(-1));

        HttpClient httpClient = SetupClient(expiredToken);

        StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The token expired at '{DateTime.UtcNow.AddDays(-1).ToString("MM/dd/yyyy HH:mm:ss")}'\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task When_Token_Has_Invalid_Signature_Then_Returns_401_Unauthorized()
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

        string invalidSecret = "Q5BkcNr4WncjhC1RiqwXmMF4zdFttEab";
        string tokenWithInvalidSecret = JwtHelper.Generate("TestUserId", secret: invalidSecret);

        HttpClient httpClient = SetupClient(tokenWithInvalidSecret);
        
        StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The signature key was not found\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task When_Token_Has_Invalid_Issuer_Then_Returns_401_Unauthorized()
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

        string invalidIssuer = "InvalidIssuer";
        string tokenWithInvalidIssuer = JwtHelper.Generate("TestUserId", issuer: invalidIssuer);
        
        HttpClient httpClient = SetupClient(tokenWithInvalidIssuer);
        
        StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The issuer 'InvalidIssuer' is invalid\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task When_Token_Has_Invalid_Audience_Then_Returns_401_Unauthorized()
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

        string invalidAudience = "InvalidAudience";
        string tokenWithInvalidAudience = JwtHelper.Generate("TestUserId", audience: invalidAudience);
        
        HttpClient httpClient = SetupClient(tokenWithInvalidAudience);
        
        StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The audience 'InvalidAudience' is invalid\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}