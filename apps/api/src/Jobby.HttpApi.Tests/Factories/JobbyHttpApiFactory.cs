using System.Net.Http.Headers;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using Testcontainers.MsSql;

namespace Jobby.HttpApi.Tests.Factories;

public class JobbyHttpApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private string _dbConnectionString = null!;
    private readonly MsSqlContainer _mssqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:latest")
        .WithReuse(true)
        .WithName("JobbyTestContainer")
        .Build();

    public HttpClient HttpClient { get; private set; }
    public FakeTimeProvider TimeProvider { get; } = new();

    public JobbyHttpApiFactory()
    {
        HttpClient = CreateClient();
    }

    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();
        
        _dbConnectionString = _mssqlContainer.GetConnectionString();
        
        await using JobbyDbContext context = GetDbContext();
        
        await context.Database.EnsureCreatedAsync();
    }
    
    public JobbyDbContext GetDbContext() => new(new DbContextOptionsBuilder<JobbyDbContext>()
        .UseSqlServer(_dbConnectionString).Options);

    Task IAsyncLifetime.DisposeAsync() => DisposeAsync().AsTask();

    protected override void ConfigureClient(HttpClient client)
    {
        base.ConfigureClient(client);
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", JwtHelper.Generate("TestUserId"));
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            OverrideClerkConfigurationSettings(config);
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<JobbyDbContext>();
            
            services.AddDbContext<JobbyDbContext>(options =>
            {
                options.UseSqlServer(_mssqlContainer.GetConnectionString());
            });
        });
    }

    private static void OverrideClerkConfigurationSettings(IConfigurationBuilder configuration)
    {
        configuration.AddInMemoryCollection(new Dictionary<string, string>
        {
            {"Clerk:Issuer", "TestIssuer"},
            {"Clerk:PEM-Key", JwtHelper.MockTestPemPublicKey}
        }!);
    }
}