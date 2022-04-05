using System.Runtime.CompilerServices;
using BlueHarvest.Shared.Application.Validators;
using BlueHarvest.Shared.Infrastructure.Controllers;
using BlueHarvest.Shared.Infrastructure.EventBus.Extensions;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("BlueHarvest.Api")]
namespace BlueHarvest.Shared.Infrastructure.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        services.AddTransient<IValidationFactory, ValidationFactory>();

        services.AddSwaggerGen();

        return services;
    }
}