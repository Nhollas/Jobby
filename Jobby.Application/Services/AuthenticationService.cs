using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Auth;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using Jobby.Domain.Primitives;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Jobby.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRepository<User> _userRepository;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        IJwtTokenGenerator jwtTokenGenerator,
        IHttpClientFactory httpClientFactory, 
        IRepository<User> userRepository, 
        IConfiguration configuration)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _httpClientFactory = httpClientFactory;
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<ExchangeTokenResponse> ExchangeGoogleToken(ExchangeTokenRequest request)
    {
        string accessToken = request.AccessToken;
        
        var response = new ExchangeTokenResponse();
        
        // Check if token has already expired or close to expiry, if so, refresh it.
        if (DateTimeOffset.Now.ToUnixTimeSeconds() > request.ExpiresAt - 60)
        {
            var refreshResponse = await RefreshGoogleToken(accessToken);

            // Current time + 1 hours.
            response.ExpiresAt = DateTimeOffset.Now.ToUnixTimeSeconds() + refreshResponse.ExpiresIn;
            accessToken = refreshResponse.AccessToken;
        }
        
        // fetch user info from github with request.Token
        var user = await GetGoogleUser(accessToken);
        
        // check if user exists in db
        var userExists = await _userRepository.FirstOrDefaultAsync(new GetUserFromOAuthIdSpecification(user.Sub));

        if (userExists is null)
        {
            var newUser = User.Create(user.Sub, "Google");
            await _userRepository.AddAsync(newUser);
        }

        response.BearerToken = _jwtTokenGenerator.GenerateToken(user.Sub, user.Name, response.ExpiresAt ?? request.ExpiresAt);
            
        return response;
    }

    public async Task<ExchangeTokenResponse> ExchangeGithubToken(ExchangeTokenRequest request)
    {
        string accessToken = request.AccessToken;

        var response = new ExchangeTokenResponse();

        // Check if token has already expired or close to expiry, if so, refresh it.
        if (DateTimeOffset.Now.ToUnixTimeSeconds() > request.ExpiresAt - 60)
        {
            var refreshResponse = await RefreshGithubToken(accessToken);

            // Current time + 8 hours.
            response.ExpiresAt = DateTimeOffset.Now.ToUnixTimeSeconds() + refreshResponse.ExpiresIn;
            accessToken = refreshResponse.AccessToken;
        }

        // fetch user info from github with request.Token
        var user = await GetGithubUser(accessToken);

        // check if user exists in db
        var userExists = await _userRepository.FirstOrDefaultAsync(new GetUserFromOAuthIdSpecification(user.Id.ToString()));
        
        if (userExists is null)
        {
            var newUser = User.Create(user.Id.ToString(), "Github");
            await _userRepository.AddAsync(newUser);
        }
            
        response.BearerToken = _jwtTokenGenerator.GenerateToken(user.Id.ToString(), user.Login, response.ExpiresAt ?? request.ExpiresAt);
            
        return response;
    }
    
    private async Task<GithubRefreshResponse> RefreshGithubToken(string refreshToken)
    {
        var httpClient = _httpClientFactory.CreateClient("Github");
        
        httpClient.BaseAddress = new Uri("https://github.com/");
        
        var clientId = _configuration.GetSection("Github:ClientId").Value;
        var clientSecret = _configuration.GetSection("Github:ClientSecret").Value;
        const string grantType = "refresh_token";
        
        var response = await httpClient.PostAsync($"/login/oauth/access_token?client_id={clientId}&client_secret={clientSecret}&grant_type={grantType}&refresh_token={refreshToken}", null);
        
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var content = await response.Content.ReadAsStringAsync();
        
        var refreshResponse = JsonSerializer.Deserialize<GithubRefreshResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        
        return refreshResponse;
    }

    private async Task<GoogleRefreshResponse> RefreshGoogleToken(string refreshToken)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var clientId = _configuration.GetSection("Google:ClientId").Value;
        var clientSecret = _configuration.GetSection("Google:ClientSecret").Value;
        const string grantType = "refresh_token";
        
        var response = await httpClient.PostAsync($"https://accounts.google.com/o/oauth2/token?client_id={clientId}&client_secret={clientSecret}&grant_type={grantType}&refresh_token={refreshToken}", null);
        
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var content = await response.Content.ReadAsStringAsync();
        
        var refreshResponse = JsonSerializer.Deserialize<GoogleRefreshResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        
        return refreshResponse;
    }
    
    private async Task<GithubUser> GetGithubUser(string token)
    {
        var httpClient = _httpClientFactory.CreateClient("Github");
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync("/user");
        
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var content = await response.Content.ReadAsStringAsync();
        
        var user = JsonSerializer.Deserialize<GithubUser>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        
        return user;
    }

    private async Task<GoogleUser> GetGoogleUser(string accessToken)
    {
        var httpClient = _httpClientFactory.CreateClient("Google");
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await httpClient.GetAsync("/oauth2/v3/userinfo");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var content = await response.Content.ReadAsStringAsync();
        
        var user = JsonSerializer.Deserialize<GoogleUser>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        
        return user;
    }
}
