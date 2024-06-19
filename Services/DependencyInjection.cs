using Application.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServicesToCollection(this IServiceCollection services)
    {
        services.AddScoped<ITransactionsManager, TransactionsManager>();
        services.AddScoped<IBankAccountsManager, BankAccountsManager>();
        return services;
    }
}