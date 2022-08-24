using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

internal sealed class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IRepository<Board> _boardRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateContactCommandHandler(
    IUserService userService,
    IRepository<Board> boardRepository,
    IDateTimeProvider dateTimeProvider)
    {
        _userService = userService;
        _userId = _userService.UserId();
        _boardRepository = boardRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    /*
        A Contact is created by it being added to a board entity (parent).
        The Contact can be added to multiple jobs that the board owns as well.
    */
    public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var boardSpec = new IncludeJobListSpecification(request.BoardId);

        Board board = await _boardRepository.FirstOrDefaultAsync(boardSpec, cancellationToken);

        if (board is null)
        {
            throw new NotFoundException($"A board with id {request.BoardId} could not be found.");
        }

        if (board.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        var createdContact = Contact.Create(
            _dateTimeProvider.UtcNow,
            _userId,
            request.FirstName,
            request.LastName,
            request.JobTitle,
            new Social(
                request.Socials.TwitterUrl,
                request.Socials.FacebookUrl,
                request.Socials.LinkedInUrl,
                request.Socials.GithubUrl
                ),
            request.BoardId,
            GetCompanies(request.Companies),
            GetEmails(request.Emails),
            GetPhones(request.Phones));

        board.AddContact(createdContact);

        if (request.JobIds != null)
        {
            var ownedJobIds = board.JobList
                .SelectMany(x => x.Jobs)
                .Select(x => x.Id)
                .ToList();

            var jobList = board.JobList
                .SelectMany(x => x.Jobs);

            foreach (var job in jobList)
            {
                if (ownedJobIds.Contains(job.Id))
                {
                    job.AddContact(createdContact);
                }
                continue;
            }
        }

        await _boardRepository.SaveChangesAsync(cancellationToken);

        return createdContact.Id;
    }

    private static List<Company> GetCompanies(List<CompanyDto> companies)
    {
        List<Company> companyList = new();

        foreach (CompanyDto company in companies)
        {
            companyList.Add(new Company { Name = company.Name });
        }

        return companyList;
    }

    private static List<Email> GetEmails(List<EmailDto> emails)
    {
        List<Email> emailList = new();

        foreach (EmailDto email in emails)
        {
            emailList.Add(new Email { Name = email.Name });
        }

        return emailList;
    }

    private static List<Phone> GetPhones(List<PhoneDto> phones)
    {
        List<Phone> phoneList = new();

        foreach (PhoneDto phone in phones)
        {
            phoneList.Add(new Phone { Number = phone.Number, Type = (PhoneType)phone.Type });
        }

        return phoneList;
    }
}
