
using Order.Application.Orders.Queries.GetOrdersByName;

namespace Order.API.Endpoints
{
    //public record GetOrdersByNameRequest(string name);
    public record GetOrderByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async(string orderName, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(orderName));

                var response = result.Adapt<GetOrdersByNameResult>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersByName")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Order")
            .WithDescription("Update Order");
        }
    }
}
