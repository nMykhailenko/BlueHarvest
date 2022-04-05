using BlueHarvest.Modules.Transactions.Core.Application.Common.Contracts.Database;
using BlueHarvest.Modules.Transactions.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlueHarvest.Modules.Transactions.Core.Infrastructure.Persistence.Repositories
{
	public class TransactionRepository : ITransactionRepository
	{
        private readonly TransactionDbContext _context;

        public TransactionRepository(TransactionDbContext context)
        {
            _context = context;
        }

		public async Task<Transaction> AddAsync(Transaction entity, CancellationToken ct)
		{
            var addedEntity = await _context.Transactions.AddAsync(entity, ct);

            return addedEntity.Entity;
		}

        public async Task<IEnumerable<Transaction>> GetByUserId(Guid userId, CancellationToken ct)
            => await _context.Transactions
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

		public Task SaveChangesAsync(CancellationToken ct)
            => _context.SaveChangesAsync(ct);
    }
}
