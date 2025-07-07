using System.Reflection;
using FluentValidation;
using Jobby.Application.Behaviours;
using Jobby.Application.Results;
using Jobby.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jobby.Application;

public static class CoreServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        Assembly applicationAssembly = typeof(AssemblyReference).Assembly;

        services.AddAutoMapper(cfg => cfg.AddMaps(applicationAssembly));
        services.AddScoped<IUserService, UserService>();
        services.AddHttpContextAccessor();

        services.AddScoped<IDispatcher, Dispatcher>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddValidatorsFromAssembly(applicationAssembly);

        return services;
    }
}
