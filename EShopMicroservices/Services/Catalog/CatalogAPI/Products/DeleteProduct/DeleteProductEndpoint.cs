namespace CatalogAPI.Products.DeleteProduct
{
	public record DeleteProductRequest(Guid Id);

	public record DeleteProductResponse(bool IsDeleted);


    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products/delete", async (DeleteProductRequest request, ISender sender) =>
            {
                var comand = request.Adapt<DeleteProductCommand>();

                var result = await sender.Send(comand);

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response.IsDeleted);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}

