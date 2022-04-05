using BlueHarvest.Modules.Transactions.Core.Application.Common.Contracts.Database;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Contracts;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Mappings;
using BlueHarvest.Modules.Transactions.Core.Infrastructure.Persistence;
using BlueHarvest.Modules.Transactions.Core.Infrastructure.Persistence.Repositories;
using BlueHarvest.Shared.Infrastructure.Database.Postgres.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Consumers;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Consumers.Definitions;
using BlueHarvest.Shared.Infrastructure.EventBus.Extensions;

[assembly: InternalsVisibleTo("BlueHarvest.Modules.Transactions.Api")]
namespace BlueHarvest.Modules.Transactions.Core.Application.Common.Extensions
{
	public static class ServiceCollectionExtensions
	{
        private const string TransactionDbSettingsSectionName = "TransactionDbSettings";

        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TransactionMappingProfile));
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();


            services.AddPostgres<TransactionDbContext>(TransactionDbSettingsSectionName);

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddRabbitMqWithConsumer<CreateTransactionConsumer, CreateTransactionConsumerDefinition>();

            return services;
        }
    }
}
