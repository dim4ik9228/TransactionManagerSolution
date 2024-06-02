using Domain.BankAccounts;
using Domain.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServicesToCollection(this IServiceCollection services)
    {
        services.AddSingleton<ITransactionsManager, TransactionsManager>();
        services.AddSingleton<IBankAccountsManager, BankAccountsManager>();
        return services;
    }
}