using FluentValidation;
using Jobby.Application.Abstractions.Authorization;
using Jobby.Application.Behaviours;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Jobby.Application;

public static class CoreServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = typeof(AssemblyReference).Assembly;

        services.AddAutoMapper(applicationAssembly);
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IGuidProvider, GuidProvider>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped(typeof(IResource<>), typeof(ResourceService<>));
        services.AddMediatR(applicationAssembly);
        services.AddValidatorsFromAssembly(applicationAssembly);

        return services;
    }
}
