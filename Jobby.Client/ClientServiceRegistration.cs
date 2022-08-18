using Jobby.Client.Interfaces;
using Jobby.Client.Services;
using Jobby.Client.Services.Base;
using System.Reflection;

namespace Jobby.Client;

public static class ClientServiceRegistration
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services.AddScoped<IBoardFeaturesService, BoardFeaturesService>();
        services.AddScoped<IContactFeaturesService, ContactFeaturesService>();
        services.AddScoped<IActivityFeaturesService, ActivityFeaturesService>();
        services.AddScoped<IAuthFeaturesService, AuthFeaturesService>();
        services.AddScoped<IJobFeaturesService, JobFeaturesService>();
        services.AddHttpContextAccessor();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<BearerTokenHandler>();

        return services;
    }
}