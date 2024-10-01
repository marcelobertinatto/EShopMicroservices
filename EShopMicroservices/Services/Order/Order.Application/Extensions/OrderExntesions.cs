namespace Order.Application.Extensions
{
    public static class OrderExntesions
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Domain.Models.Order> orders)
        {
            return orders.Select(order => new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto(
                        order.ShippingAddress.FirstName,
                        order.ShippingAddress.LastName,
                        order.ShippingAddress.EmailAddress,
                        order.ShippingAddress.AddressLine,
                        order.ShippingAddress.Country,
                        order.ShippingAddress.State,
                        order.ShippingAddress.Zipcode
                    ),
                    BillingAddress: new AddressDto(
                        order.BillingAddress.FirstName,
                        order.BillingAddress.LastName,
                        order.BillingAddress.EmailAddress,
                        order.BillingAddress.AddressLine,
                        order.BillingAddress.Country,
                        order.BillingAddress.State,
                        order.BillingAddress.Zipcode
                    ),
                    Payment: new PaymentDto(
                        CardName: order.Payment.CardName,
                        CardNumber: order.Payment.CardNumber,
                        Expiration: order.Payment.Expiration,
                        Cvv: order.Payment.CVV,
                        PaymentMethod: order.Payment.PaymentMethod
                    ),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(x =>
                        new OrderItemDto(x.OrderId.Value,x.ProductId.Value,
                        x.Quantity,x.Price)
                    ).ToList() 
            ));
        }
    }
}