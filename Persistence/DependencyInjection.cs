using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Commands.BankAccountCommands;
using Persistence.Commands.TransactionCommands;
using Persistence.Queries.BankAccountQueries;
using Persistence.Queries.TransactionQueries;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServicesToCollection(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<TransactionManagerDbContext>(option =>
        {
            option.UseNpgsql(config.GetConnectionString("PostgreSqlConnection"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<AddBankAccountCommand>();
        services.AddScoped<UpdateBankAccountBalanceCommand>();
        services.AddScoped<AddTransactionCommand>();
        services.AddScoped<RemoveTransactionByIdCommand>();
        services.AddScoped<GetBankAccountByIdQuery>();
        services.AddScoped<GetTransactionsByBankAccountIdQuery>();

        return services;
    }
}