using BlueHarvest.Modules.Transactions.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlueHarvest.Modules.Transactions.Core.Infrastructure.Persistence
{
	public class TransactionDbContext : DbContext
	{
		public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Transaction> Transactions { get; set; } = null!;
	}
}
