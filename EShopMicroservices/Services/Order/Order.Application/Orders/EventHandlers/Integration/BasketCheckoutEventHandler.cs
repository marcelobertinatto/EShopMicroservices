using BuildingBlocks.Messaging.Events;
using MassTransit;
using Order.Application.Orders.Commands.CreateOrder;
using Order.Domain.Enums;

namespace Order.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, 
    ILogger<BasketCheckoutEventHandler> logger)
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", 
            context.Message.GetType().Name);

            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        // Create full order with incoming event data
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId, new Guid("39ccfd6f-41f6-446c-aa5f-35e816881a8c"), 2, 500),
                new OrderItemDto(orderId, new Guid("9299a4e8-9c13-4eab-8903-9ba301e0335d"), 1, 400)
            ]);

        return new CreateOrderCommand(orderDto);
    }
    }
}