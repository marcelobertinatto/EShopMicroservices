using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category,
        string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsOk);
    internal class UpdateProductCommandHandler(IDocumentSession session,
        ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, 
            CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);
                var product = await session.LoadAsync<Product>(command.Id, 
                    cancellationToken);

                session.Update(product);
                await session.SaveChangesAsync(cancellationToken);

                return new UpdateProductResult(true);
            }
            catch (Exception)
            {
                return new UpdateProductResult(false);
            }
        }
    }
}
