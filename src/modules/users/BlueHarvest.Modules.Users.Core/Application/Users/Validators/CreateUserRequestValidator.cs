using BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;
using FluentValidation;

namespace BlueHarvest.Modules.Users.Core.Application.Users.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator() 
    {
        RuleFor(x => x.CustomerId).NotNull().GreaterThan(0);
        RuleFor(x => x.FirstName).NotNull().NotEmpty();
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.InitialCredit).GreaterThanOrEqualTo(0);
    }
}