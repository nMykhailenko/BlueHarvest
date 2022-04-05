using BlueHarvest.Modules.Transactions.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHarvest.Modules.Transactions.Core.Application.Common.Contracts.Database
{
	public interface ITransactionRepository
	{
		Task<IEnumerable<Transaction>> GetByUserId(Guid userId, CancellationToken ct);
		Task<Transaction> AddAsync(Transaction entity, CancellationToken ct);
		Task SaveChangesAsync(CancellationToken ct);
	}
}
