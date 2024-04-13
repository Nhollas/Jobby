using System.Net;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Features.ActivityFeatures.Create.Fixtures;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class GivenRequestWithJobToLink : IClassFixture<JobToLinkFixture>
{
    private readonly JobbyHttpApiFactory _factory;
    private readonly JobToLinkFixture _fixture;

    public GivenRequestWithJobToLink(
        JobbyHttpApiFactory factory, 
        JobToLinkFixture fixture)
    {
        _factory = factory;
        _fixture = fixture;
    }

    private HttpResponseMessage Response => _fixture.Response;
    private CreateActivityCommand Body => _fixture.Body;
    private ActivityDto? ReturnedActivity => _fixture.ReturnedActivity;
    
    private string ExpectedName => ActivityConstants.TypesDictionary.GetValueOrDefault((int)Body.Type, ActivityConstants.TypesDictionary[0]);
    
    [Fact]
    public void Then_Returns_201_Created()
    {
        Response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public void Then_Returns_Created_Activity_With_Job_Linked()
    {
        ReturnedActivity.Should().NotBeNull();
        
        using (new AssertionScope())
        {
            ReturnedActivity.Title.Should().Be(Body.Title);
            ReturnedActivity.Name.Should().Be(ExpectedName);
            ReturnedActivity.StartDate.Should().Be(Body.StartDate);
            ReturnedActivity.EndDate.Should().Be(Body.EndDate);
            ReturnedActivity.Note.Should().Be(Body.Note);
            ReturnedActivity.Completed.Should().Be(Body.Completed);
            ReturnedActivity.BoardReference.Should().Be(Body.BoardReference);
            ReturnedActivity.Job.Reference.Should().Be(Body.JobReference);
            ReturnedActivity.Type.Should().Be((int)Body.Type);
        }
    }
    
    [Fact]
    public async Task Then_Inserts_Activity_In_Database_And_Has_Job_Linked()
    {
        await using JobbyDbContext updatedContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        Assert.NotNull(ReturnedActivity);

        Activity? createdActivity = await updatedContext.Activities.Include(activity => activity.Job).FirstOrDefaultAsync(activity =>
            activity.Reference == ReturnedActivity.Reference);

        createdActivity.Should().NotBeNull();
        
        using (new AssertionScope())
        {
            createdActivity?.Title.Should().Be(Body.Title);
            createdActivity?.Name.Should().Be(ExpectedName);
            createdActivity?.StartDate.Should().Be(Body.StartDate);
            createdActivity?.EndDate.Should().Be(Body.EndDate);
            createdActivity?.Note.Should().Be(Body.Note);
            createdActivity?.Completed.Should().Be(Body.Completed);
            createdActivity?.BoardReference.Should().Be(Body.BoardReference);
            createdActivity?.Job.Reference.Should().Be(Body.JobReference);
            createdActivity?.Type.Should().Be((int)Body.Type);
        }
    }
}