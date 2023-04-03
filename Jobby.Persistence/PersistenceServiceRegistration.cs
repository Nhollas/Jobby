﻿using Jobby.Application.Abstractions.Specification;
using Jobby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobby.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<JobbyDbContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("JobbyConnection")));
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));

        return services;
    }
}
