using BlueHarvest.Modules.Transactions.Core.Application.Transactions.Contracts;
using BlueHarvest.Shared.Application.Models.RequestModels.Transactions;
using MassTransit;

namespace BlueHarvest.Modules.Transactions.Core.Application.Transactions.Consumers;

public class CreateTransactionConsumer : IConsumer<CreateTransactionRequest>
{
    private readonly ITransactionService _transactionService;

    public CreateTransactionConsumer(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public Task Consume(ConsumeContext<CreateTransactionRequest> context)
    {
        return _transactionService.CreateTransactionAsync(context.Message, default);
    }
}