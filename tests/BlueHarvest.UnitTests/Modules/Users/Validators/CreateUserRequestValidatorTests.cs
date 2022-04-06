using System.Threading.Tasks;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;
using BlueHarvest.Modules.Users.Core.Application.Users.Validators;
using Bogus;
using FluentAssertions;
using Xunit;

namespace BlueHarvest.UnitTests.Modules.Users.Validators;

public class CreateUserRequestValidatorTests
{
    private readonly CreateUserRequestValidator _sut;

    public CreateUserRequestValidatorTests()
    {
        _sut = new CreateUserRequestValidator();
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturn_ModelIsValid()
    {
        // arrange
        const int customerId = 1;
        var createUserRequest = new Faker<CreateUserRequest>()
            .StrictMode(true)
            .RuleFor(u => u.FirstName, _ => _.Person.FirstName)
            .RuleFor(u => u.LastName, _ => _.Person.LastName)
            .RuleFor(u => u.CustomerId, _ => customerId)
            .RuleFor(u => u.InitialCredit, _ => _.Finance.Amount())
            .Generate();
        
        // act
        var actual = await _sut.ValidateAsync(createUserRequest);

        // assert
        actual.Should().NotBeNull();
        actual.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturn_ModelIsNotValid_When_RequiredPropertiesAreMissing()
    {
        // arrange
        var createUserRequest = new Faker<CreateUserRequest>()
            .RuleFor(u => u.FirstName, _ => _.Person.FirstName)
            .RuleFor(u => u.LastName, _ => _.Person.LastName)
            .RuleFor(u => u.InitialCredit, _ => _.Finance.Amount())
            .Generate();    
        
        // act
        var actual = await _sut.ValidateAsync(createUserRequest);

        // assert
        actual.Should().NotBeNull();
        actual.IsValid.Should().BeFalse();
    }
}