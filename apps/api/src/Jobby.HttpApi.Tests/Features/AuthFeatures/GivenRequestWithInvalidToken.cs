using System.Net;
using System.Net.Http.Headers;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;

namespace Jobby.HttpApi.Tests.Features.AuthFeatures;

[Collection("SqlCollection")]
public class GivenRequestWithInvalidToken(JobbyHttpApiFactory factory)
{
    [Fact]
    public async Task WhenTokenHasExpired_ThenReturns401Unauthorized()
    {
        string expiredToken = JwtHelper.Generate("TestUserId", expires: DateTime.UtcNow.AddDays(-1));

        HttpClient httpClient = factory.CreateClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", expiredToken);
        
        HttpResponseMessage response = await httpClient.GetAsync("/boards");
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The token expired at '{DateTime.UtcNow.AddDays(-1):MM/dd/yyyy HH:mm:ss}'\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task WhenTokenHasInvalidSignature_ThenReturns401Unauthorized()
    {
        string invalidPem = "-----BEGIN RSA PRIVATE KEY-----\nMIICXAIBAAKBgQCyIdKk9Izy0ykEfKY4gKD4hoK92R4c1fwkkHVmuie/uoRdmpSE\nWgDVM5HAAEOKoQx2QumNq4O10YI3XtxY4207XH+Oo7oKiLuofMsoGX+4ttM+GtJu\ndDRV8RW7N3qMsl4iW+jLG7F60+j+oC2zciO8adr6RPLJO8si5fxwCNCRVwIDAQAB\nAoGAa7ioebipw+6OT3hRzLl8ns45IjW6UBXXdQUm+gh5ISjaAwEH70G+Xy/gZAvE\nzIC32Ix+UH34GGuqI3HqLpbZ5piAxDf1DhRkLI8+53hWU/B9LOE+gE9n4U76975s\nlnCXELUxzyh8yInXqiWR4UR1s3MMj5az946c4Td4dsNnZoECQQDrFJuxr3BiM1Ii\ngGPyNVlV7mYSwleusRsI+3JFvdF5JwzDTKoQ9QnEPdwY70edcQ4KU6M4gvQ1nTdA\nDE8AonrVAkEAwfveOA2p5vUOZgAADNjQZne47uAFUZp6u6+VXPzHBhKX16M7bujF\nxB4jee/vC6K2ypmduC7V8AicqPna/J3ZewJAQyfV+oKl1kfW9Og8pRq8dKHwIvfF\n2K/bi0tZr7a0Oqn/KWOjScjWi2sojy78BGwhmK2f+Srf3NkWyYM6pnHEKQJAdqau\nV67T23bM5crePP1pCyPzs/jGiBFrPN27CHN88NPymG05by9lt/2PSYheuMk/8VBg\nzkWnifhaimi5b4bFfQJBAJl2GMl7Vaj1DJEol+dsuvsZ+UHdb/D5vco33LT7FCm7\n9oCgjaOgAQQhXZ0/nTRscfCwsN+yI9mkeS0wLpqDIXo=\n-----END RSA PRIVATE KEY-----";
        string tokenWithInvalidSecret = JwtHelper.Generate("TestUserId", privatePemKey: invalidPem);

        HttpClient httpClient = factory.CreateClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenWithInvalidSecret);
        
        HttpResponseMessage response = await httpClient.GetAsync("/boards");
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The signature key was not found\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task WhenTokenHasInvalidIssuer_ThenReturns401Unauthorized()
    {
        const string invalidIssuer = "InvalidIssuer";
        string tokenWithInvalidIssuer = JwtHelper.Generate("TestUserId", issuer: invalidIssuer);
        
        HttpClient httpClient = factory.CreateClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenWithInvalidIssuer);
        
        HttpResponseMessage response = await httpClient.GetAsync("/boards");
        
        string expectedErrorMessage = $"Bearer error=\"invalid_token\", error_description=\"The issuer 'InvalidIssuer' is invalid\"";
        
        Assert.Equal(expectedErrorMessage, response.Headers.WwwAuthenticate.ToString());
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}