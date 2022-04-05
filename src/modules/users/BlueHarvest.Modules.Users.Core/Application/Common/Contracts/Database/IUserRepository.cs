using BlueHarvest.Modules.Users.Core.Domain.Entities;

namespace BlueHarvest.Modules.Users.Core.Application.Common.Contracts.Database;

public interface IUserRepository
{
    Task<User?> FindByUserIdAsync(Guid userId, CancellationToken ct);
    Task<User?> FindByCustomerIdAsync(int customerId, CancellationToken ct);
    Task<User> AddAsync(User entity, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}