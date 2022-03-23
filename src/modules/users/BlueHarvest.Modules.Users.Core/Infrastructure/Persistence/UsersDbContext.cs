using BlueHarvest.Modules.Users.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlueHarvest.Modules.Users.Core.Infrastructure.Persistence;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<User> Users { get; set; } = null!;
}