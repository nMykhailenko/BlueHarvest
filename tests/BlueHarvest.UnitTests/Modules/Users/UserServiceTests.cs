using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Models.ResponseModels;
using BlueHarvest.Modules.Users.Core.Application.Common.Contracts.Database;
using BlueHarvest.Modules.Users.Core.Application.Users;
using BlueHarvest.Modules.Users.Core.Application.Users.Contracts;
using BlueHarvest.Modules.Users.Core.Application.Users.Mappings;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.ResponseModels;
using BlueHarvest.Modules.Users.Core.Domain.Entities;
using BlueHarvest.Shared.Application.Models.ErrorModels;
using BlueHarvest.Shared.Application.Models.SuccessModels;
using BlueHarvest.Shared.Application.Validators;
using Bogus;
using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using OneOf;
using Xunit;

namespace BlueHarvest.UnitTests.Modules.Users;

public class UserServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILogger<UserService>> _loggerMock;
    private readonly Mock<IValidationFactory> _validationFactoryMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;

    private IUserService _sut;

    public UserServiceTests()
    {
        var mapperConfiguration = new MapperConfiguration(
            cfg => cfg.AddProfile<UserMappingProfile>());
        _mapper = mapperConfiguration.CreateMapper();

        _loggerMock = new Mock<ILogger<UserService>>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _validationFactoryMock = new Mock<IValidationFactory>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _sut = new UserService(
            _loggerMock.Object, 
            _mapper, 
            _userRepositoryMock.Object, 
            _validationFactoryMock.Object, _publishEndpointMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturn_EntityNotFound_ErrorModel_If_UserNotExists()
    {
        // arrange
        var userId = Guid.NewGuid();
        _userRepositoryMock
            .Setup(x => x.FindByUserIdAsync(userId, CancellationToken.None))
            .ReturnsAsync((User?) null);
        var expectedResponse = new EntityNotFound($"User with id: {userId} not found");
        
        // act
        var actual = await _sut.GetByIdAsync(userId, CancellationToken.None);

        // assert
        _userRepositoryMock
            .Verify(x => x.FindByUserIdAsync(userId, CancellationToken.None), Times.Once);
        actual.Value.Should().Be(expectedResponse);
    }
    
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturn_UserResponse()
    {
        // arrange
        var userId = Guid.NewGuid();
        var user = new Faker<User>()
            .StrictMode(true)
            .RuleFor(u => u.FirstName, _ => _.Person.FirstName)
            .RuleFor(u => u.LastName, _ => _.Person.LastName)
            .RuleFor(u => u.CustomerId, _ => _.IndexFaker)
            .RuleFor(u => u.Balance, _ => _.Finance.Amount())
            .RuleFor(u => u.Id, _ => Guid.NewGuid())
            .Generate();
        _userRepositoryMock
            .Setup(x => x.FindByUserIdAsync(userId, CancellationToken.None))
            .ReturnsAsync(user);
        var expectedResponse = _mapper.Map<UserResponse>(user);
        
        // act
        var actual = await _sut.GetByIdAsync(userId, CancellationToken.None);

        // assert
        _userRepositoryMock
            .Verify(x => x.FindByUserIdAsync(userId, CancellationToken.None), Times.Once);
        actual.Value.Should().BeEquivalentTo(expectedResponse);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldReturn_EntityNotValid()
    {
        // arrange
        var createUserRequest = new Faker<CreateUserRequest>()
            .StrictMode(true)
            .RuleFor(u => u.FirstName, _ => _.Person.FirstName)
            .RuleFor(u => u.LastName, _ => _.Person.LastName)
            .RuleFor(u => u.CustomerId, _ => _.IndexFaker)
            .RuleFor(u => u.InitialCredit, _ => _.Finance.Amount())
            .Generate();

        var expectedResponse = new EntityNotValid("Entity_not_valid");
        _validationFactoryMock
            .Setup(x => x.ValidateAsync(createUserRequest))
            .ReturnsAsync(expectedResponse);
    
        // act
        var actual = await _sut.CreateAsync(createUserRequest, CancellationToken.None);
    
        // assert
        actual.Value.Should().Be(expectedResponse);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldReturn_EntityAlreadyExists()
    {
        // arrange
        var createUserRequest = new Faker<CreateUserRequest>()
            .StrictMode(true)
            .RuleFor(u => u.FirstName, _ => _.Person.FirstName)
            .RuleFor(u => u.LastName, _ => _.Person.LastName)
            .RuleFor(u => u.CustomerId, _ => _.IndexFaker)
            .RuleFor(u => u.InitialCredit, _ => _.Finance.Amount())
            .Generate();
        
        _validationFactoryMock
            .Setup(x => x.ValidateAsync(createUserRequest))
            .ReturnsAsync(new ValidationSuccess());

        _userRepositoryMock
            .Setup(x => x.FindByCustomerIdAsync(createUserRequest.CustomerId, CancellationToken.None))
            .ReturnsAsync(new User());

        var expectedResponse = new EntityAlreadyExists($"User with customerId: {createUserRequest.CustomerId}");
    
        // act
        var actual = await _sut.CreateAsync(createUserRequest, CancellationToken.None);
    
        // assert
        actual.Value.Should().BeEquivalentTo(expectedResponse);
    }
}