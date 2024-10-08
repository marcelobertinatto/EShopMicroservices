using MassTransit;
using Microsoft.FeatureManagement;

namespace Order.Application.Orders.EventHandlers
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handled: {DomainEvent}", domainEvent.GetType().Name);
            
            if(await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
                await publishEndpoint.Publish(orderCreatedIntegrationEvent,cancellationToken);
            }            
        }
    }
}