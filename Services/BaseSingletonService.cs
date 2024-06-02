using Microsoft.Extensions.DependencyInjection;

namespace Services;

internal abstract class BaseSingletonService(IServiceScopeFactory serviceScopeFactory)
{
    protected TService GetRequiredService<TService>() where TService : notnull
    {
        var scope = serviceScopeFactory.CreateScope();

        var requiredService = scope
            .ServiceProvider
            .GetRequiredService<TService>();

        return requiredService;
    }
}