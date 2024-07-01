using System.Net.Http.Headers;
using System.Text;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Testcontainers.MsSql;

namespace Jobby.HttpApi.Tests.Factories;

public class JobbyHttpApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private string _dbConnectionString = string.Empty;
    private readonly MsSqlContainer _mssqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:latest")
        .WithCleanUp(true)
        .WithName("JobbyTestContainer")
        .Build();

    public HttpClient HttpClient { get; private set; }

    public JobbyHttpApiFactory()
    {
        HttpClient = SetupClient();
    }

    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();
        
        _dbConnectionString = _mssqlContainer.GetConnectionString();
        
        await using JobbyDbContext context = GetDbContext();
        
        await context.Database.EnsureCreatedAsync();
    }
    
    public JobbyDbContext GetDbContext()
    {
        return new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_dbConnectionString).Options);
    }

    public new async Task DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }

    public HttpClient SetupClient(string token)
    {
        HttpClient client = CreateClient();
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        
        return client;
    }

    private HttpClient SetupClient()
    {
        HttpClient client = CreateClient();
        
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer",  JwtHelper.Generate("TestUserId"));
        
        return client;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    SecurityKey issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtHelper.TestSigningKey));

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "TestIssuer",
                        ValidAudience = "TestAudience",
                        ValidateIssuer = true,
                        ValidateAudience = true,
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
}