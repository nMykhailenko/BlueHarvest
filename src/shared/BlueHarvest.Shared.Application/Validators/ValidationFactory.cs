using System.Text.Json;
using BlueHarvest.Shared.Application.Models.ErrorModels;
using BlueHarvest.Shared.Application.Models.ResponseModels;
using BlueHarvest.Shared.Application.Models.SuccessModels;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OneOf;

namespace BlueHarvest.Shared.Application.Validators;

public class ValidationFactory : IValidationFactory
{
    private readonly IServiceProvider _serviceProvider;


    public ValidationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<OneOf<ValidationSuccess, EntityNotValid>> ValidateAsync<TRequest>(TRequest request)
    {
        var validator = _serviceProvider.GetRequiredService<IValidator<TRequest>>();
        
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.IsValid)
        {
            return new ValidationSuccess();
        }
        
        var errors = validationResult.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            )
            .Select(x => new ValidationResponse(x.Key, string.Join(",", x.Value)));

        return new EntityNotValid(JsonSerializer.Serialize(errors));
    }
}