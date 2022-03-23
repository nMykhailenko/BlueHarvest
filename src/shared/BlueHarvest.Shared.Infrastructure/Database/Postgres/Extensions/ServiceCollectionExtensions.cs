using BlueHarvest.Shared.Application.Extensions;
using BlueHarvest.Shared.Infrastructure.Database.Postgres.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlueHarvest.Shared.Infrastructure.Database.Postgres.Extensions;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddPostgres(this IServiceCollection services, string sectionName)
    {
        var options = services.GetSettings<PostgreSettings>(sectionName);
        services.AddSingleton(options);

        return services;
    }

    public static IServiceCollection AddPostgres<T>(
        this IServiceCollection services,
        string sectionName) where T : DbContext
    {
        var options = services.GetSettings<PostgreSettings>(sectionName);
        services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));

        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        dbContext.Database.Migrate();

        return services;
    }
}