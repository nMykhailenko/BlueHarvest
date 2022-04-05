using System;
using AutoMapper;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Models.ResponseModels;
using BlueHarvest.Modules.Users.Core.Application.Users.Mappings;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.ResponseModels;
using BlueHarvest.Modules.Users.Core.Domain.Entities;
using BlueHarvest.Shared.Application.Models.Enums.Transactions;
using Bogus;
using FluentAssertions;
using Xunit;

namespace BlueHarvest.UnitTests.Modules.Users;

public class UserMappingProfileTests
{
    private readonly MapperConfiguration _mapperConfiguration;
    private readonly IMapper _sut;

    public UserMappingProfileTests()
    {
        _mapperConfiguration = new MapperConfiguration(
            cfg => cfg.AddProfile<UserMappingProfile>());
        _sut = _mapperConfiguration.CreateMapper();
    }

    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        // assert
        _mapperConfiguration.AssertConfigurationIsValid();
    }

    [Fact]
    public void AutoMapper_ShouldMap_CreateUserRequest_To_User_InProperWay()
    {
        // arrange
        var createUserRequest = new Faker<CreateUserRequest>()
            .StrictMode(true)
            .RuleFor(u => u.FirstName, _ => _.Person.FirstName)
            .RuleFor(u => u.LastName, _ => _.Person.LastName)
            .RuleFor(u => u.CustomerId, _ => _.IndexFaker)
            .RuleFor(u => u.InitialCredit, _ => _.Finance.Amount())
            .Generate();
    
        // act
        var actual = _sut.Map<User>(createUserRequest);
        
        // assert
        actual.Should().NotBeNull();
        actual.Balance.Should().Be(createUserRequest.InitialCredit);
        actual.CustomerId.Should().Be(createUserRequest.CustomerId);
        actual.FirstName.Should().Be(createUserRequest.FirstName);
        actual.LastName.Should().Be(createUserRequest.LastName);
    }
    
    [Fact]
    public void AutoMapper_ShouldMap_User_To_UserResponse_InProperWay()
    {
        // arrange
        var user = new Faker<User>()
            .StrictMode(true)
            .RuleFor(u => u.FirstName, _ => _.Person.FirstName)
            .RuleFor(u => u.LastName, _ => _.Person.LastName)
            .RuleFor(u => u.CustomerId, _ => _.IndexFaker)
            .RuleFor(u => u.Balance, _ => _.Finance.Amount())
            .RuleFor(u => u.Id, _ => Guid.NewGuid())
            .Generate();
    
        // act
        var actual = _sut.Map<UserResponse>(user);
        
        // assert
        actual.Should().NotBeNull();
        actual.Id.Should().Be(user.Id);
        actual.Balance.Should().Be(user.Balance);
        actual.CustomerId.Should().Be(user.CustomerId);
        actual.FirstName.Should().Be(user.FirstName);
        actual.LastName.Should().Be(user.LastName);
    }
}