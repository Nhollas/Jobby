using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;
using static Jobby.Domain.Static.ContactConstants;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

internal sealed class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IRepository<Board> _boardRepository;
    private readonly IRepository<Contact> _contactRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateContactCommandHandler(
    IUserService userService,
    IRepository<Board> boardRepository,
    IDateTimeProvider dateTimeProvider,
    IRepository<Contact> contactRepository,
    IGuidProvider guidProvider)
    {
        _userService = userService;
        _userId = _userService.UserId();
        _boardRepository = boardRepository;
        _dateTimeProvider = dateTimeProvider;
        _contactRepository = contactRepository;
        _guidProvider = guidProvider;
    }

    public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var boardSpec = new GetBoardWithJobsSpec(request.BoardId);

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
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.FirstName,
            request.LastName,
            request.JobTitle,
            request.Location,
            new Social(
                request.Socials.TwitterUrl,
                request.Socials.FacebookUrl,
                request.Socials.LinkedInUrl,
                request.Socials.GithubUrl
                ),
            board,
            GetCompanies(request.Companies),
            GetEmails(request.Emails),
            GetPhones(request.Phones));

        if (request.JobIds != null)  
        {
            var ownedJobIds = board.JobList
                .SelectMany(x => x.Jobs)
                .Select(x => x.Id)
                .ToList();

            var requestJobList = board.JobList
                .SelectMany(x => x.Jobs)
                .Where(x => request.JobIds.Contains(x.Id))
                .ToList();

            foreach (var job in requestJobList)
            {
                if (!ownedJobIds.Contains(job.Id))
                {
                    throw new NotAuthorisedException(_userId);
                }
            }

            createdContact.SetJobs(requestJobList);
        }

        await _contactRepository.AddAsync(createdContact, cancellationToken); 

        return createdContact.Id;
    }

    private static List<Company> GetCompanies(List<CompanyDto> companies)
    {
        List<Company> companyList = new();

        foreach (CompanyDto company in companies)
        {
            companyList.Add(new Company(Guid.NewGuid(), company.Name));
        }

        return companyList;
    }

    private static List<Email> GetEmails(List<EmailDto> emails)
    {
        List<Email> emailList = new();

        foreach (EmailDto email in emails)
        {
            emailList.Add(new Email(Guid.NewGuid(), email.Name));
        }

        return emailList;
    }

    private static List<Phone> GetPhones(List<PhoneDto> phones)
    {
        List<Phone> phoneList = new();

        foreach (PhoneDto phone in phones)
        {
            phoneList.Add(new Phone(Guid.NewGuid(), phone.Number, (PhoneType)phone.Type));
        }

        return phoneList;
    }
}
