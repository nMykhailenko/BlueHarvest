using BlueHarvest.Shared.Application.Models.Enums.Transactions;

namespace BlueHarvest.Modules.Transactions.Core.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }
    public TransactionOperation Operation { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}