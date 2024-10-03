
using Order.Application.Orders.Queries.GetOrdersByCustomer;

namespace Order.API.Endpoints
{
    //public record GetOrderByCustomerRequest(Guid CustomerId);
    public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);
    public class GetOrderByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));

                var response = result.Adapt<GetOrdersByCustomerResult>();

                return Results.Ok(response);
            })
            .WithName("GetOrderByCustomer")
            .Produces<GetOrderByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Order By Customer")
            .WithDescription("Get Order By Customer");
        }
    }
}
