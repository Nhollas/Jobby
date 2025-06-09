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
using Respawn;

namespace Jobby.HttpApi.Tests.Setup;

public class JobbyHttpApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private string _dbConnectionString = null!;
    private Respawner _respawner = null!;
    private readonly MsSqlContainer _mssqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-CU13-ubuntu-22.04")
        .WithReuse(true)
        .WithName("JobbyTestContainer")
        .Build();

    public string UserId { get; private set; } = "TestUserId";
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

        JobbyDbContext dbContext = GetDbContext();
        await dbContext.Database.EnsureCreatedAsync();

        _respawner = await Respawner.CreateAsync(_dbConnectionString, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer
        });
    }

    public JobbyDbContext GetDbContext() => new(new DbContextOptionsBuilder<JobbyDbContext>()
        .UseSqlServer(_dbConnectionString).Options);

    async Task IAsyncLifetime.DisposeAsync() => await _respawner.ResetAsync(_dbConnectionString);

    protected override void ConfigureClient(HttpClient client)
    {
        base.ConfigureClient(client);

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", JwtHelper.Generate(UserId));
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