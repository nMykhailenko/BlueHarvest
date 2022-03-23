using System.Runtime.CompilerServices;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using BlueHarvest.Modules.Users.Core.Application.Common.Contracts.Database;
using BlueHarvest.Modules.Users.Core.Application.Users;
using BlueHarvest.Modules.Users.Core.Application.Users.Contracts;
using BlueHarvest.Modules.Users.Core.Application.Users.Mappings;
using BlueHarvest.Modules.Users.Core.Infrastructure.Persistence;
using BlueHarvest.Modules.Users.Core.Infrastructure.Persistence.Repositories;
using BlueHarvest.Shared.Infrastructure.Database.Postgres.Extensions;

[assembly: InternalsVisibleTo("BlueHarvest.Modules.Users.Api")]
namespace BlueHarvest.Modules.Users.Core.Application.Common.Extensions;

public static class ServiceCollectionExtensions
{
    private const string UserDbSettingsSectionName = "UserDbSettings";

    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMappingProfile));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        
        
        services.AddPostgres<UsersDbContext>(UserDbSettingsSectionName);

        services.AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssemblyContaining<UsersDbContext>();
            fv.DisableDataAnnotationsValidation = true;
        });
        
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        return services;
    }
}