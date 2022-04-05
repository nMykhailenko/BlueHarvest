using MassTransit;

namespace BlueHarvest.Modules.Transactions.Core.Application.Transactions.Consumers.Definitions;

public class CreateTransactionConsumerDefinition : ConsumerDefinition<CreateTransactionConsumer>
{
    public CreateTransactionConsumerDefinition()
    {
        EndpointName = "create-transaction";
        ConcurrentMessageLimit = 8;
    }

    protected void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<CreateTransactionConsumerDefinition> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100,200,500,800,1000));

        endpointConfigurator.UseInMemoryOutbox();
    }
}