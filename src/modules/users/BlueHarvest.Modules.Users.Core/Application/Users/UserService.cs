using AutoMapper;
using BlueHarvest.Modules.Users.Core.Application.Common.Contracts.Database;
using BlueHarvest.Modules.Users.Core.Application.Users.Contracts;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.ResponseModels;
using BlueHarvest.Modules.Users.Core.Domain.Entities;
using BlueHarvest.Shared.Application.Models.Enums.Transactions;
using BlueHarvest.Shared.Application.Models.ErrorModels;
using BlueHarvest.Shared.Application.Models.RequestModels.Transactions;
using BlueHarvest.Shared.Application.Validators;
using MassTransit;
using Microsoft.Extensions.Logging;
using OneOf;

namespace BlueHarvest.Modules.Users.Core.Application.Users;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IValidationFactory _validationFactory;
    private readonly IPublishEndpoint _publishEndpoint;


    public UserService(
        ILogger<UserService> logger,
        IMapper mapper,
        IUserRepository userRepository, 
        IValidationFactory validationFactory, 
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
        _validationFactory = validationFactory;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<OneOf<UserResponse, EntityNotFound>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var user = await _userRepository.FindByUserIdAsync(id, ct);
        if (user is null)
        {
            return new EntityNotFound($"User with id: {id} not found");
        }

        var response = _mapper.Map<UserResponse>(user);
        return response;
    }

    public async Task<OneOf<UserResponse, EntityNotValid, EntityAlreadyExists>> CreateAsync(
        CreateUserRequest request, 
        CancellationToken ct)
    {
        var validationResult = await _validationFactory.ValidateAsync(request);
        return await validationResult.Match(
            async _ =>
            {
                var currentUser = await _userRepository.FindByCustomerIdAsync(request.CustomerId, ct);
                if(currentUser is not null)
				{
                    var entityAlreadyExistsMessage = $"User with customerId: {request.CustomerId}";
                    _logger.LogInformation(entityAlreadyExistsMessage);

                    var entityAlreadyExistsResponse = new EntityAlreadyExists(entityAlreadyExistsMessage);
                    return entityAlreadyExistsResponse;
				}

                var userToAdd = _mapper.Map<User>(request);
                var addedUser = await _userRepository.AddAsync(userToAdd, ct);
                await _userRepository.SaveChangesAsync(ct);

                var createTransactionRequest = new CreateTransactionRequest
                {
                    Amount = request.InitialCredit,
                    Operation = TransactionOperation.Deposit,
                    UserId = addedUser.Id
                };
                await _publishEndpoint.Publish(createTransactionRequest, ct);
                
                var response = _mapper.Map<UserResponse>(addedUser);

                return response;
            }, 
            entityNotValid => Task.FromResult<OneOf<UserResponse, EntityNotValid, EntityAlreadyExists>>(entityNotValid));    
    }
}