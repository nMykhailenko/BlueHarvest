using BlueHarvest.Shared.Application.Models.Enums.Transactions;

namespace BlueHarvest.Modules.Transactions.Core.Application.Transactions.Models.ResponseModels
{
	public record TransactionResponse
	{
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
        public Guid UserId { get; init; }
        public string Operation { get; init; } = null!;
        public DateTimeOffset CreatedAt { get; init; }
    }
}
