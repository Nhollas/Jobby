using System.Net;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.ContactFeatures.Delete;

public class WhenContactIsLinkedToJobFixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    public string ContactReference { get; private set; } = null!;
    public Job[] ExistingLinkedJobs { get; private set; } = [];
    
    public async Task InitializeAsync()
    {
        await using JobbyDbContext dbContext = factory.GetDbContext();
        Board board = await new TestDataBuilder(factory)
            .CreateBoard()
                .WithContact()
                .WithJob()
                .BuildAsync();

        Contact contact = board.Contacts.First();

        ExistingLinkedJobs =
        [
            .. dbContext.Jobs
                .Include(j => j.JobContacts)
                .Include(x => x.Contacts)
                .Where(j => j.JobContacts.Any(jc => jc.ContactReference == contact.Reference))
        ];
        Response = await factory.HttpClient.DeleteAsync($"/contact/{contact.Reference}");
        ContactReference = contact.Reference;
    }

    public Task DisposeAsync() => Task.CompletedTask;
}

[Collection("SqlCollection")]
public class WhenContactIsLinkedToJob(JobbyHttpApiFactory factory, WhenContactIsLinkedToJobFixture fixture) : IClassFixture<WhenContactIsLinkedToJobFixture>
{

    [Fact]
    public void ThenReturns200Ok()
    {
        fixture.Response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ThenRemovesContactInDatabase()
    {
        await using JobbyDbContext context = factory.GetDbContext();

        Contact? deletedContact = await context.Contacts
            .FirstOrDefaultAsync(c => c.Reference == fixture.ContactReference);

        deletedContact.Should().BeNull();
    }

    [Fact]
    public void ThenJobsAreUnlinkedFromContact()
    {
        foreach (Job job in fixture.ExistingLinkedJobs)
        {
            job.Contacts.Should().NotContain(c => c.Reference == fixture.ContactReference);
            job.JobContacts.Should().NotContain(jc => jc.ContactReference == fixture.ContactReference);
        }
    }
}