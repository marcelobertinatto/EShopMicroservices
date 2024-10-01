using BuildingBlocks.Pagination;
using Order.Application.Orders.Queries.GetOrdersByName;

public record GetOrdersQuery(PaginationRequest PaginationRequest)
    : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);

