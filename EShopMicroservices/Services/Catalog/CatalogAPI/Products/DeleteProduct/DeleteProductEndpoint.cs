using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Products.DeleteProduct
{
	public record DeleteProductRequest(Guid Id);

	public record DeleteProductResponse(bool IsDeleted);


    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products", async ([FromBody]DeleteProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<DeleteProductCommand>();

                var result = await sender.Send(command);

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

