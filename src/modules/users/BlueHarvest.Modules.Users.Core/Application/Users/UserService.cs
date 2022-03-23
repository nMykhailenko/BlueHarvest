using AutoMapper;
using BlueHarvest.Modules.Users.Core.Application.Common.Contracts.Database;
using BlueHarvest.Modules.Users.Core.Application.Users.Contracts;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.ResponseModels;
using BlueHarvest.Modules.Users.Core.Domain.Entities;
using BlueHarvest.Shared.Application.Models.ErrorModels;
using BlueHarvest.Shared.Application.Validators;
using OneOf;

namespace BlueHarvest.Modules.Users.Core.Application.Users;

internal class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IValidationFactory _validationFactory;

    public UserService(
        IMapper mapper,
        IUserRepository userRepository, 
        IValidationFactory validationFactory)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _validationFactory = validationFactory;
    }
    
    public async Task<OneOf<UserResponse, EntityNotValid>> CreateAsync(CreateUserRequest request, CancellationToken ct)
    {
        var validationResult = await _validationFactory.ValidateAsync(request);
        return await validationResult.Match<Task<OneOf<UserResponse, EntityNotValid>>>(
            async _ =>
            {
                var userToAdd = _mapper.Map<User>(request);

                var addedUser = await _userRepository.AddAsync(userToAdd, ct);
                await _userRepository.SaveChangesAsync(ct);

                var response = _mapper.Map<UserResponse>(addedUser);

                return response;
            }, 
            entityNotValid => Task.FromResult<OneOf<UserResponse, EntityNotValid>>(entityNotValid));    
    }
}