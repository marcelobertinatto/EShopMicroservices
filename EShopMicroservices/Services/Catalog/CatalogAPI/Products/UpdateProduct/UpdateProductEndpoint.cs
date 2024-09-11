
using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category,
        string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsOk);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var comand = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(comand);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response.IsOk);
            });
        }
    }
}
