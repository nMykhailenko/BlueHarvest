using BlueHarvest.Modules.Transactions.Core.Application.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BlueHarvest.Api")]
namespace BlueHarvest.Modules.Transactions.Api
{
	internal static class TransactionsModule
	{
        public const string ModulePath = "transactions";

        public static IServiceCollection AddTransactionsModule(this IServiceCollection services)
        {
            services.AddCore();

            return services;
        }

        public static IApplicationBuilder UseTransactionsModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
