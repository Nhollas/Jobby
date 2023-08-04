using System.Net.Http.Headers;
using Jobby.Persistence.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using Xunit;

namespace Jobby.HttpApi.Tests.Factories;

public class JobbyHttpApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public string DbConnectionString { get; set; } = string.Empty;
    
    private readonly MsSqlContainer _mssqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:latest")
        .WithCleanUp(true)
        .WithName("JobbyTestContainer")
        .Build();
    
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", false, false)
        .Build();
    public HttpClient HttpClient { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();
        
        HttpClient = CreateClient();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Jwt:TestToken"]);
        
        DbConnectionString = _mssqlContainer.GetConnectionString();
    }

    public new async Task DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // TODO: Fake our authentication too. So we don't need to use a real clerk JWT token.
            
            services.RemoveDbContext<JobbyDbContext>();
            
            services.AddDbContext<JobbyDbContext>(options =>
            {
                options.UseSqlServer(_mssqlContainer.GetConnectionString());
            });

            services.EnsureDbCreatedAsync<JobbyDbContext>().ConfigureAwait(true);
        });
    }
}