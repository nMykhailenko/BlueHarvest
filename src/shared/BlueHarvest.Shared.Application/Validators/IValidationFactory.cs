using OneOf;
using BlueHarvest.Shared.Application.Models.ErrorModels;
using BlueHarvest.Shared.Application.Models.SuccessModels;

namespace BlueHarvest.Shared.Application.Validators;

public interface IValidationFactory
{
    Task<OneOf<ValidationSuccess, EntityNotValid>> ValidateAsync<TRequest>(TRequest request);
}