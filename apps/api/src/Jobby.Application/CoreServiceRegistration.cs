using FluentValidation;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jobby.Application;

public static class CoreServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = typeof(AssemblyReference).Assembly;

        services.AddAutoMapper(applicationAssembly);
        services.AddScoped<IUserService, UserService>();
        services.AddHttpContextAccessor();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IGuidProvider, GuidProvider>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddValidatorsFromAssembly(applicationAssembly);

        return services;
    }
}
