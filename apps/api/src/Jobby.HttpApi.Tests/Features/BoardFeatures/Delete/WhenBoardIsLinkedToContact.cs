using System.Net;
using Jobby.Domain.Entities;
using Jobby.HttpApi.Tests.Factories;
using Jobby.HttpApi.Tests.Helpers;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Jobby.HttpApi.Tests.Features.BoardFeatures.Delete;

public class WhenBoardIsLinkedToContactFixture(JobbyHttpApiFactory factory) : IAsyncLifetime
{
    public HttpResponseMessage Response { get; private set; } = new();
    public string BoardReference { get; private set; } = null!;
    public string ContactReference { get; private set; } = null!;
    
    public async Task InitializeAsync()
    {
        Board board = await new TestDataBuilder(factory)
            .CreateBoard()
            .WithContact()
            .BuildAsync();

        BoardReference = board.Reference;
        ContactReference = board.Contacts.First().Reference;
        Response = await factory.HttpClient.DeleteAsync($"/board/{board.Reference}");
    }

    public Task DisposeAsync() => Task.CompletedTask;
}

[Collection("SqlCollection")]
public class WhenBoardIsLinkedToContact(JobbyHttpApiFactory factory, WhenBoardIsLinkedToContactFixture fixture) : IClassFixture<WhenBoardIsLinkedToContactFixture>
{
    [Fact]
    public void ThenReturns200Ok()
    {
        fixture.Response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task ThenRemovesBoardInDatabase()
    {
        await using JobbyDbContext context = factory.GetDbContext();

        Board? deletedBoard = await context.Boards
            .FirstOrDefaultAsync(c => c.Reference == fixture.BoardReference);

        deletedBoard.Should().BeNull();
    }

    [Fact]
    public async Task ThenContactHasBoardUnlinkedInDatabase()
    {
        await using JobbyDbContext context = factory.GetDbContext();

        Contact existingLinkedContact = await context.Contacts
            .Include(x => x.Board)
            .SingleAsync(c => c.Reference == fixture.ContactReference);

        existingLinkedContact.Board.Should().BeNull();
        existingLinkedContact.BoardReference.Should().BeNull();
        existingLinkedContact.BoardId.Should().BeNull();
    }
}