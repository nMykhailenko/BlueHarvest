using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;

[assembly:InternalsVisibleTo("BlueHarvest.Api")]
namespace BlueHarvest.Shared.Infrastructure.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}