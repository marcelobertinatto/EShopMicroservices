using BuildingBlocks.Exceptions;

namespace Order.Application.Orders.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid id) 
            : base("Order", id)
        {
            
        }
    }
}