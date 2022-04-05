using System.Runtime.CompilerServices;
using BlueHarvest.Modules.Users.Core.Application.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("BlueHarvest.Api")]
namespace BlueHarvest.Modules.Users.Api;

internal static class UsersModule
{
    public const string ModulePath = "users";
    
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        services.AddCore();
            
        return services;
    }

    public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
    {
        return app;
    }
}