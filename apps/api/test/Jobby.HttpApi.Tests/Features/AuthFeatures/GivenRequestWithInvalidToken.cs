using System.Net;
using System.Text;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;

namespace Jobby.HttpApi.Tests.Features.AuthFeatures;

[Collection("SqlCollection")]
public class GivenRequestWithInvalidToken(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task WhenTokenHasExpired_ThenReturns401Unauthorized()
    {
        const string json = 
        """
        {
          "title": "k",
          "boardId": "2d97a782-24a6-2962-e7cd-3a0c759c3d29",
          "type": 41,
          "startDate": "2023-08-15T23:00:00.000Z",
          "endDate": "2023-08-15T23:00:00.000Z",
          "note": "Cool Note!"
        }
        """;

        string expiredToken = JwtHelper.Generate("TestUserId", expires: DateTime.UtcNow.AddDays(-1));

        HttpClient httpClient = factory.SetupClient(expiredToken);

        StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The token expired at '{DateTime.UtcNow.AddDays(-1):MM/dd/yyyy HH:mm:ss}'\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task WhenTokenHasInvalidSignature_ThenReturns401Unauthorized()
    {
        const string json =
        """
        {
          "title": "k",
          "boardId": "2d97a782-24a6-2962-e7cd-3a0c759c3d29",
          "type": 41,
          "startDate": "2023-08-15T23:00:00.000Z",
          "endDate": "2023-08-15T23:00:00.000Z",
          "note": "Cool Note!"
        }
        """;

        string invalidSecret = "Q5BkcNr4WncjhC1RiqwXmMF4zdFttEab";
        string tokenWithInvalidSecret = JwtHelper.Generate("TestUserId", secret: invalidSecret);

        HttpClient httpClient = factory.SetupClient(tokenWithInvalidSecret);
        
        StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The signature key was not found\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task WhenTokenHasInvalidIssuer_ThenReturns401Unauthorized()
    {
        const string json =
        """
        {
          "title": "k",
          "boardId": "2d97a782-24a6-2962-e7cd-3a0c759c3d29",
          "type": 41,
          "startDate": "2023-08-15T23:00:00.000Z",
          "endDate": "2023-08-15T23:00:00.000Z",
          "note": "Cool Note!"
        }
        """;

        const string invalidIssuer = "InvalidIssuer";
        string tokenWithInvalidIssuer = JwtHelper.Generate("TestUserId", issuer: invalidIssuer);
        
        HttpClient httpClient = factory.SetupClient(tokenWithInvalidIssuer);
        
        StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await httpClient.PostAsync("/activity", body);
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The issuer 'InvalidIssuer' is invalid\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task WhenTokenHasInvalidAudience_ThenReturns401Unauthorized()
    {
        const string json = 
        """
        {
          "title": "k",
          "boardId": "2d97a782-24a6-2962-e7cd-3a0c759c3d29",
          "type": 41,
          "startDate": "2023-08-15T23:00:00.000Z",
          "endDate": "2023-08-15T23:00:00.000Z",
          "note": "Cool Note!"
        }
        """;

        const string invalidAudience = "InvalidAudience";
        string tokenWithInvalidAudience = JwtHelper.Generate("TestUserId", audience: invalidAudience);
        
        HttpClient httpClient = factory.SetupClient(tokenWithInvalidAudience);
        
        StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await httpClient.PostAsync("/activity", body);
        
        const string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The audience 'InvalidAudience' is invalid\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}