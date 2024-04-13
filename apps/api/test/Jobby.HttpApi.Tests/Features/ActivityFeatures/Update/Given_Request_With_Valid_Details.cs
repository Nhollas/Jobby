using System.Net;
using System.Net.Http.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Features.ActivityFeatures.Update.Fixtures;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ActivityFeatures.Update;

[Collection("SqlCollection")]
public class GivenRequestWithValidDetails : IClassFixture<UpdateActivityTestFixture>
{
    private readonly UpdateActivityTestFixture _fixture;
    private readonly JobbyHttpApiFactory _factory;
    
    public GivenRequestWithValidDetails(
        UpdateActivityTestFixture fixture, 
        JobbyHttpApiFactory factory)
    {
        _fixture = fixture;
        _factory = factory;
    }

    private HttpResponseMessage Response => _fixture.Response;
    private UpdateActivityCommand Body => _fixture.Body;
    
    private Activity PreLoadedActivity => _fixture.PreloadedActivity;
    
    [Fact]
    public void Then_Returns_200_OK()
    {
        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
    }

    [Fact]
    public async Task Then_Returns_Updated_Activity()
    {
        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);

        ActivityDto? returnedActivity = await Response.Content.ReadFromJsonAsync<ActivityDto>();
        
        Assert.NotNull(returnedActivity);

        Assert.Multiple(
            () => returnedActivity.Title.Should().Be(Body.Title),
            () => returnedActivity.StartDate.Should().Be(Body.StartDate),
            () => returnedActivity.EndDate.Should().Be(Body.EndDate),
            () => returnedActivity.Note.Should().Be(Body.Note),
            () => returnedActivity.Completed.Should().Be(Body.Completed),
            () => returnedActivity.Type.Should().Be(Body.Type)
        );
    }

    [Fact]
    public async Task Then_Updates_Activity_In_Database()
    {
        await using JobbyDbContext updatedContext = new JobbyDbContext(new DbContextOptionsBuilder<JobbyDbContext>()
            .UseSqlServer(_factory.DbConnectionString).Options);
        
        Activity? updatedActivity = await updatedContext.Activities.FirstOrDefaultAsync(x => x.Id == PreLoadedActivity.Id);

        Assert.NotNull(updatedActivity);
        
        Assert.Multiple(
            () => updatedActivity.Title.Should().Be(Body.Title),
            () => updatedActivity.StartDate.Should().Be(Body.StartDate),
            () => updatedActivity.EndDate.Should().Be(Body.EndDate),
            () => updatedActivity.Note.Should().Be(Body.Note),
            () => updatedActivity.Completed.Should().Be(Body.Completed),
            () => updatedActivity.Type.Should().Be(Body.Type)
        );
    }
}