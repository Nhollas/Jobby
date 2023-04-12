﻿using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;
using static Jobby.Domain.Static.ContactConstants;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

internal sealed class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactDto>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IRepository<Board> _boardRepository;
    private readonly IRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public CreateContactCommandHandler(
    IUserService userService,
    IRepository<Board> boardRepository,
    IDateTimeProvider dateTimeProvider,
    IRepository<Contact> contactRepository,
    IGuidProvider guidProvider,
    IMapper mapper)
    {
        _userId = userService.UserId();
        _boardRepository = boardRepository;
        _dateTimeProvider = dateTimeProvider;
        _contactRepository = contactRepository;
        _guidProvider = guidProvider;
        _mapper = mapper;
    }

    public async Task<ContactDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        Board board = await ResourceProvider<Board>
            .GetBySpec(_boardRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetBoardWithJobsSpecification(request.BoardId))
            .Check(_userId, cancellationToken);

        Contact createdContact = Contact.Create(
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
            List<Job> jobsToLink = board.JobLists
                .SelectMany(x => x.Jobs)
                .Where(x => request.JobIds.Contains(x.Id))
                .ToList();

            createdContact.SetJobs(jobsToLink);
        }

        await _contactRepository.AddAsync(createdContact, cancellationToken);

        return _mapper.Map<ContactDto>(createdContact);
    }

    private List<Company> GetCompanies(List<string> companies)
    {
        return (from string company in companies select new Company(_guidProvider.Create(), company))
            .ToList();
    }

    private List<Email> GetEmails(List<string> emails)
    {
        return (from string email in emails select new Email(_guidProvider.Create(), email))
            .ToList();
    }

    private List<Phone> GetPhones(List<PhoneDto> phones)
    {
        return (from PhoneDto phone in phones select new Phone(_guidProvider.Create(), phone.Number, (PhoneType)phone.Type))
            .ToList();
    }
}
