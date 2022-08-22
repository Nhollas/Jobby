﻿using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;

public class IncludeJobListSpecification : Specification<Board>
{
    public IncludeJobListSpecification(Guid BoardId)
    {
        Query
            .Where(b => b.Id == BoardId)
            .Include(x => x.JobList)
                .ThenInclude(x => x.Jobs)
                    .ThenInclude(x => x.Activities)
            .Include(x => x.JobList)
                .ThenInclude(x => x.Jobs)
                    .ThenInclude(x => x.JobContacts)
            .Include(x => x.Activities)
            .Include(x => x.Contacts)
                .ThenInclude(x => x.JobContacts);
    }
}