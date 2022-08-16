using Jobby.Client.Interfaces;
using Jobby.Client.Services;
using Jobby.Client.Services.Base;

namespace Jobby.Client;

public static class ClientServiceRegistration
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services.AddScoped<IBoardFeaturesService, BoardFeaturesService>();
        services.AddScoped<IContactFeaturesService, ContactFeaturesService>();
        services.AddScoped<IActivityFeaturesService, ActivityFeaturesService>();

        services.AddHttpContextAccessor();
        services.AddTransient<BearerTokenHandler>();

        return services;
    }
}