using CatalogAPI.Products.DeleteProduct;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category,
        string Description, string ImageFile, decimal Price);

    public record UpdateProductResponse(bool IsUpdated);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var comand = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(comand);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response.IsUpdated);
            }).WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}
