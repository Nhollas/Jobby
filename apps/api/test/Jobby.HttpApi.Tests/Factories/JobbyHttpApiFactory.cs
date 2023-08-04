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
    public string JobbyDbConnectionString { get; set; } = string.Empty;
    private readonly MsSqlContainer _mssqlContainer;
    private readonly IConfiguration _configuration;
    public HttpClient HttpClient { get; private set; } = null!;

    public JobbyHttpApiFactory()
    {
        _mssqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:latest")
            .WithCleanUp(true)
            .WithName("JobbyTestContainer")
            .Build();
        
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, false)
            .Build();
    }

    public async Task InitializeDatabaseAsync()
    {
        using var context = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(JobbyDbConnectionString)
            .Options);
        
        await context.Database.EnsureCreatedAsync();
    }
    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();
        
        HttpClient = CreateClient();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Jwt:TestToken"]);
        
        JobbyDbConnectionString = _mssqlContainer.GetConnectionString();
    }

    public new async Task DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<JobbyDbContext>();
            
            services.AddDbContext<JobbyDbContext>(options =>
            {
                options.UseSqlServer(_mssqlContainer.GetConnectionString());
            });

            services.EnsureDbCreatedAsync<JobbyDbContext>().ConfigureAwait(true);
        });
    }
}