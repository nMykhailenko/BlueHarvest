using BlueHarvest.Modules.Users.Core.Application.Users.Models.RequestModels;
using BlueHarvest.Modules.Users.Core.Application.Users.Models.ResponseModels;
using BlueHarvest.Shared.Application.Models.ErrorModels;
using OneOf;

namespace BlueHarvest.Modules.Users.Core.Application.Users.Contracts;

public interface IUserService
{
    Task<OneOf<UserResponse, EntityNotValid>> CreateAsync(CreateUserRequest request, CancellationToken ct);
}