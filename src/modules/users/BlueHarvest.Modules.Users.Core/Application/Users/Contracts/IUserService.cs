using BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.ResponseModels;
using BlueHarvest.Modules.Users.Core.Domain.Entities;
using BlueHarvest.Shared.Application.Models.ErrorModels;
using OneOf;

namespace BlueHarvest.Modules.Users.Core.Application.Users.Contracts;

public interface IUserService
{
    Task<OneOf<UserResponse, EntityNotFound>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<OneOf<UserResponse, EntityNotValid, EntityAlreadyExists>> CreateAsync(CreateUserRequest request, CancellationToken ct);
}