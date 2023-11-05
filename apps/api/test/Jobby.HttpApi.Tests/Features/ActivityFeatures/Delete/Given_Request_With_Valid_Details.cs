using System.Net;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete.Fixtures;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Delete;

[Collection("SqlCollection")]
public class Given_Request_With_Valid_Details: IClassFixture<DeleteActivityWithValidDetailsFixture>
{
    private readonly JobbyHttpApiFactory _factory;
    private readonly DeleteActivityWithValidDetailsFixture _fixture;

    public Given_Request_With_Valid_Details(
        JobbyHttpApiFactory factory, 
        DeleteActivityWithValidDetailsFixture fixture)
    {
        _factory = factory;
        _fixture = fixture;
    }
    
    private HttpResponseMessage Response => _fixture.Response;
    private string ActivityReference => _fixture.ActivityReference;
    
        [Fact]
    public void Then_Returns_200_OK()
    {
        Response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Then_Removes_Activity_In_Database()
    {
        await using var updatedContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);

        var deletedActivity = await updatedContext.Activities.FirstOrDefaultAsync(act => act.Reference == ActivityReference);

        deletedActivity.Should().BeNull();
    }
}