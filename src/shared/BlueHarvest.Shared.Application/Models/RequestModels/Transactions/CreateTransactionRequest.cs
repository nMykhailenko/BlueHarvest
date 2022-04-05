using BlueHarvest.Shared.Application.Models.Enums.Transactions;

namespace BlueHarvest.Shared.Application.Models.RequestModels.Transactions
{
	public record CreateTransactionRequest
	{
		public Guid UserId { get; init; }
		public decimal Amount { get; init; }
		public TransactionOperation Operation { get; init; }
	}
}
