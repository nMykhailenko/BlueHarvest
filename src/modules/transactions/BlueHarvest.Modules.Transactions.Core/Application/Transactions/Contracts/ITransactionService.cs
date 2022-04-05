using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Models.ResponseModels;
using BlueHarvest.Shared.Application.Models.RequestModels.Transactions;

namespace BlueHarvest.Modules.Transactions.Core.Application.Transactions.Contracts
{
	public interface ITransactionService
	{
		Task<IReadOnlyCollection<TransactionResponse>> GetTransactionsForUserAsync(Guid userId, CancellationToken ct);
		Task<TransactionResponse> CreateTransactionAsync(CreateTransactionRequest request, CancellationToken ct);
	}
}
