using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("BlueHarvest.Shared.Infrastructure")]
namespace BlueHarvest.Shared.Application.Extensions;

internal static class ServiceCollectionExtensions
{
    public static T GetSettings<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var section = configuration.GetSection(sectionName);
        var settings = new T();
        
        section.Bind(settings);
            
        return settings;
    }
}