using Order.Domain.Enums;

namespace Order.Application.DTOs
{
   public record OrderDto(   
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressDto ShippingAddress,
        AddressDto BillingAddress,
        PaymentDto Payment,
        OrderStatus Status,
        List<OrderItemDto> OrderItems);
}