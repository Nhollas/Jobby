using System.Net;
using FluentAssertions;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Static;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Features.ActivityFeatures.Create.Fixtures;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Create;

[Collection("SqlCollection")]
public class Given_Request_With_Valid_Details : IClassFixture<ValidDetailsTestFixture>
{
    private readonly JobbyHttpApiFactory _factory;
    private readonly ValidDetailsTestFixture _fixture;

    public Given_Request_With_Valid_Details(
        JobbyHttpApiFactory factory, 
        ValidDetailsTestFixture fixture)
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
        Assert.Equal(HttpStatusCode.Created, Response.StatusCode);
    }

    [Fact]
    public void Then_Returns_Created_Activity()
    {
        Assert.NotNull(ReturnedActivity);

        Assert.Multiple(
            () => ReturnedActivity.Title.Should().Be(Body.Title),
            () => ReturnedActivity.Name.Should().Be(ExpectedName),
            () => ReturnedActivity.StartDate.Should().Be(Body.StartDate),
            () => ReturnedActivity.EndDate.Should().Be(Body.EndDate),
            () => ReturnedActivity.Note.Should().Be(Body.Note),
            () => ReturnedActivity.Completed.Should().Be(Body.Completed),
            () => ReturnedActivity.BoardReference.Should().Be(Body.BoardReference),
            () => ReturnedActivity.Job.Should().BeNull(),
            () => ReturnedActivity.Type.Should().Be((int)Body.Type)
        );
    }

    [Fact]
    public async Task Then_Inserts_Activity_In_Database()
    {
        await using var updatedContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        Assert.NotNull(ReturnedActivity);

        var createdActivity = await updatedContext.Activities.Include(activity => activity.Job).FirstOrDefaultAsync(activity =>
            activity.Reference == ReturnedActivity.Reference);

        Assert.NotNull(createdActivity);
        
        Assert.Multiple(
            () => createdActivity.Title.Should().Be(Body.Title),
            () => createdActivity.Name.Should().Be(ExpectedName),
            () => createdActivity.StartDate.Should().Be(Body.StartDate),
            () => createdActivity.EndDate.Should().Be(Body.EndDate),
            () => createdActivity.Note.Should().Be(Body.Note),
            () => createdActivity.Completed.Should().Be(Body.Completed),
            () => createdActivity.BoardReference.Should().Be(Body.BoardReference),
            () => createdActivity.Job.Should().BeNull(),
            () => createdActivity.Type.Should().Be((int)Body.Type)
        );
    }
}