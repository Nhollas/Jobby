using System.Net.Http.Headers;
using System.Text;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtHelper.Generate("TestUserId"));
        
        DbConnectionString = _mssqlContainer.GetConnectionString();
        
        await using var context = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(DbConnectionString).Options);
        
        await context.Database.EnsureCreatedAsync();

        await SeedDataHelper.AddSeedDataAsync(this);
    }

    public new async Task DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveJwtAuthentication();
            
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        SecurityKey issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9f7b309b-1dcc-4a96-a292-dbe6e830d8c3"));

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = "TestIssuer",
                            ValidAudience = "TestAudience",
                            ValidateIssuer = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ClockSkew = TimeSpan.Zero,
                            IssuerSigningKey = issuerSigningKey
                        };
                    }
                );
            
            services.RemoveDbContext<JobbyDbContext>();
            
            services.AddDbContext<JobbyDbContext>(options =>
            {
                options.UseSqlServer(_mssqlContainer.GetConnectionString());
            });
        });
    }
    
    public List<Board> SeededBoards { get; set; } = new List<Board>();
}